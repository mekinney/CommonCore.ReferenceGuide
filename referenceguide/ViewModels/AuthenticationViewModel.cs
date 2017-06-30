using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AuthenticationViewModel : ObservableViewModel
    {
        private string accessToken;

        public string AccessToken
        {
            get { return accessToken; }
            set { SetProperty(ref accessToken, value); }
        }

        public ICommand GoogleAuth { get; set; }
        public ICommand FaceBookAuth { get; set; }
        public ICommand MicrosoftAuth { get; set; }

        public AuthenticationViewModel()
        {
            GoogleAuth = new RelayCommand((obj) =>
            {
                AccessToken = string.Empty;
                AuthenticateUser(AuthenticatorType.Google);
            });

            FaceBookAuth = new RelayCommand((obj) =>
            {
                AccessToken = string.Empty;
                AuthenticateUser(AuthenticatorType.Facebook);
            });

            MicrosoftAuth = new RelayCommand(async (obj) =>
            {
                AccessToken = string.Empty;
                await AuthenticateMicrosoftUser();
            });

        }

        private void AuthenticateUser(AuthenticatorType type)
        {
            var authenticator = AuthenticatorService.GetAuthenticator(type, (acct) =>
                {
                    if (acct != null)
                    {
                        Console.WriteLine($"**************************** {acct.Properties["access_token"]} *****************************");
                        AccessToken = acct.Properties["access_token"];
                    }
                    else
                    {
                        Console.WriteLine($"**************************** Account was NULL *****************************");
                        AccessToken = "Token was null";
                    }

                    if (Xamarin.Forms.Device.RuntimePlatform == "iOS")
                        DependencyService.Get<IViewStack>().DismissTopView();

                },
                (error) =>
                {
                    AccessToken = error.Message;
                });

            AuthenticationState.Authenticator = authenticator;
            PresentUILoginScreen(authenticator);
        }


        private async void PresentUILoginScreen(OAuth2Authenticator authenticator)
        {
            if (authenticator != null)
            {
                var ap = new Xamarin.Auth.XamarinForms.AuthenticatorPage()
                {
                    Authenticator = authenticator,
                };

                await this.Navigation.PushAsync(ap);
            }

        }

        /// <summary>
        /// Authenticates the microsoft user.
        /// see the following article -> 
        ///     https://blog.xamarin.com/enterprise-apps-made-easy-updated-libraries-apis/
        ///     https://github.com/AzureAD/microsoft-authentication-library-for-dotnet
        /// </summary>
        private async Task AuthenticateMicrosoftUser()
        {

            var identityClientApp = new PublicClientApplication(AppData.Instance.MicrosoftAppId);
            identityClientApp.RedirectUri = $"msal{AppData.Instance.MicrosoftAppId}://auth";
            string[] scopes = { "User.Read", "User.ReadBasic.All ", "Mail.Send" };
            UIParent uiParent = null;

#if __ANDROID__
            if(Xamarin.Forms.Device.RuntimePlatform =="Android")
                uiParent =new UIParent(Xamarin.Forms.Forms.Context as Android.App.Activity);
#endif
			try
			{
                var Client = new GraphServiceClient("https://graph.microsoft.com/v1.0",
                      new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    var tokenRequest = await identityClientApp.AcquireTokenAsync(scopes, uiParent).ConfigureAwait(false);
                    AccessToken = tokenRequest.AccessToken;
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", tokenRequest.AccessToken);
                }));

				var userInfo = await Client.Me.Request().GetAsync();
		
			}
			catch (MsalException ex)
			{
                AccessToken = ex.Message;
			}
        }

    }
}