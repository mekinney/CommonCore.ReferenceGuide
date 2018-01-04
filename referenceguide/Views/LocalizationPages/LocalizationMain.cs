using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public static class Lang
    {
        static ILocalizationService srv { get; set; } = (ILocalizationService)CoreDependencyService.GetService<ILocalizationService, LocalizationService>(true);
        public static string GreetingText { get; } = srv["Greeting"];
        public static string LoginText { get; } = srv["Login"];
        public static string PasswordText { get; } = srv["Password"];
        public static string UserNameText { get; } = srv["UserName"];

    }
    public class LocalizationMain: CorePage<LocalizationViewModel>
    {
        public LocalizationMain()
        {
            this.Title = "Localization";

            var lblGreeting = new Label()
            {
                FontSize = 50,
                HorizontalTextAlignment = TextAlignment.Center,
                Text = Lang.GreetingText
                                                       
            };
            //lblGreeting.SetBinding(Label.TextProperty,"GreetingText");

            var lblUserName = new Label()
            {
                Text = Lang.UserNameText
            };
            //lblUserName.SetBinding(Label.TextProperty, "UserNameText");

            var txtUserName = new CoreMaskedEntry()
            {

            };


            var lblPassword = new Label()
            {
                Text = Lang.PasswordText
            };
            //lblPassword.SetBinding(Label.TextProperty, "PasswordText");

            var txtPassword = new CoreMaskedEntry()
            {
                IsPassword = true
            };

            var btnLogin = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                AutomationId = "errors",
                Text = Lang.LoginText
            };
            //btnLogin.SetBinding(CoreButton.TextProperty, "LoginText");

            Content = new CompressedStackLayout()
            {
                Padding = 20,
                Spacing = 15,
                Children = { lblGreeting, lblUserName, txtUserName, lblPassword, txtPassword, btnLogin }
            };
        }
    }
}
