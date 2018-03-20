using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class LocalizationViewModel : CoreViewModel
    {
        public string GreetingText { get; set; }
        public string LoginText { get; set; }
        public string PasswordText { get; set; }
        public string UserNameText { get; set; }

        public override void OnViewMessageReceived(string key, object obj)
        {
            switch (key)
            {
                case CoreSettings.LoadResources:
                    if (LocalizationService.IsLoaded)
                        InitLocalizationString();
                    break;
                case CoreSettings.LocalizationLoaded:
                    InitLocalizationString();
                    break;
            }
        }

        private void InitLocalizationString()
        {
            GreetingText = LocalizationService["Greeting"];
            LoginText = LocalizationService["Login"];
            PasswordText = LocalizationService["Password"];
            UserNameText = LocalizationService["UserName"];
        }
    }
}
