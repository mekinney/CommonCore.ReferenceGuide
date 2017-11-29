using System;
using Xamarin.Forms.CommonCore;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Auth;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

#if __ANDROID__
using Plugin.CurrentActivity;
#endif

namespace referenceguide
{
    public class OAuthBusinessLayer : CoreBusiness
	{
		public OAuth2Authenticator GetAuthenticator(AuthenticatorType type, Action<string> tokenCallBack)
		{
			var authenticator = AuthenticatorService.GetAuthenticator(type, (acct) =>
			{
				if (acct != null)
				{
					Console.WriteLine($"**************************** {acct.Properties["access_token"]} *****************************");
					tokenCallBack?.Invoke(acct.Properties["access_token"]);
				}
				else
				{
					Console.WriteLine($"**************************** Account was NULL *****************************");
					tokenCallBack?.Invoke("Token was null");
				}

				if (Xamarin.Forms.Device.RuntimePlatform == "iOS")
					DependencyService.Get<IViewStack>().DismissTopView();

			},
			(error) =>
			{
				Log.LogException(error);
				tokenCallBack?.Invoke(error.Message);
			});

			AuthenticationState.Authenticator = authenticator;
			return authenticator;
		}

		/// <summary>
		/// Authenticates the microsoft user.
		/// see the following article -> 
		///     https://blog.xamarin.com/enterprise-apps-made-easy-updated-libraries-apis/
		///     https://github.com/AzureAD/microsoft-authentication-library-for-dotnet
		/// </summary>
		public async Task AuthenticateMicrosoftUser(Action<string> tokenCallBack)
		{
			var identityClientApp = new PublicClientApplication(CoreSettings.Config.SocialMedia.MicrosoftAppId);
			identityClientApp.RedirectUri = $"msal{CoreSettings.Config.SocialMedia.MicrosoftAppId}://auth";
			string[] scopes = { "User.Read", "User.ReadBasic.All ", "Mail.Send" };
			UIParent uiParent = null;

#if __ANDROID__


            if(Xamarin.Forms.Device.RuntimePlatform =="Android")
                uiParent =new UIParent(CrossCurrentActivity.Current.Activity as Android.App.Activity);
#endif
			try
			{
				var Client = new GraphServiceClient("https://graph.microsoft.com/v1.0",
					  new DelegateAuthenticationProvider(async (requestMessage) =>
					  {
						  var tokenRequest = await identityClientApp.AcquireTokenAsync(scopes, uiParent).ConfigureAwait(false);
						  tokenCallBack?.Invoke(tokenRequest.AccessToken);
						  requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", tokenRequest.AccessToken);
					  }));

				var userInfo = await Client.Me.Request().GetAsync();

			}
			catch (MsalException ex)
			{
				Log.LogException(ex);
				tokenCallBack?.Invoke(ex.Message);
			}
		}
	}
}
