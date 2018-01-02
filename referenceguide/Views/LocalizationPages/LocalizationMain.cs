using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class LocalizationMain: CorePage<LocalizationViewModel>
    {
        public LocalizationMain()
        {
            this.Title = "Localization";

            var lblGreeting = new Label()
            {
                FontSize=50,
                HorizontalTextAlignment = TextAlignment.Center
            };
            lblGreeting.SetBinding(Label.TextProperty,"GreetingText");

            var lblUserName = new Label()
            {

            };
            lblUserName.SetBinding(Label.TextProperty, "UserNameText");

            var txtUserName = new CoreMaskedEntry()
            {

            };


            var lblPassword = new Label()
            {

            };
            lblPassword.SetBinding(Label.TextProperty, "PasswordText");

            var txtPassword = new CoreMaskedEntry()
            {
                IsPassword = true
            };

            var btnLogin = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                AutomationId = "errors",
            };
            btnLogin.SetBinding(CoreButton.TextProperty, "LoginText");

            Content = new CompressedStackLayout()
            {
                Padding = 20,
                Spacing = 15,
                Children = { lblGreeting, lblUserName, txtUserName, lblPassword, txtPassword, btnLogin }
            };
        }
    }
}
