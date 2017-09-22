using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AuthenticationViewModel : ObservableViewModel
    {
        public string AccessToken { get; set; }
        public ICommand GoogleAuth { get; set; }
        public ICommand FaceBookAuth { get; set; }
        public ICommand MicrosoftAuth { get; set; }

        public AuthenticationViewModel()
        {
            GoogleAuth = new RelayCommand((obj) => { GoogleAuthMethod(); });
            FaceBookAuth = new RelayCommand((obj) => { FaceBookAuthMethod(); });
            MicrosoftAuth = new RelayCommand(async (obj) => { await MicrosoftAuthMethod(); });

        }

        private async Task MicrosoftAuthMethod()
        {
            AccessToken = string.Empty;
            await AuthenticateMicrosoftUser();
        }

        private void FaceBookAuthMethod()
        {
            AccessToken = string.Empty;
            AuthenticateUser(AuthenticatorType.Facebook);
        }

        private void GoogleAuthMethod()
        {
            AccessToken = string.Empty;
            AuthenticateUser(AuthenticatorType.Google);
        }

        private void AuthenticateUser(AuthenticatorType type)
        {
            var authenticator = OAuthBLL.GetAuthenticator(type, (token) =>
            {
                AccessToken = token;
            });

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

        private async Task AuthenticateMicrosoftUser()
        {
            await OAuthBLL.AuthenticateMicrosoftUser((token) =>
            {
                AccessToken = token;
            });
        }

    }
}