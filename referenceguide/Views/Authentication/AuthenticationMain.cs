using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AuthenticationMain :CorePage<AuthenticationViewModel>
    {
        public AuthenticationMain()
        {
            this.Title = "Authentications";

			var googleAuth = new CoreButton()
			{
				Style = AppStyles.LightOrange,
				Text = "Google Authentication",
				AutomationId = "googleAuth"
			};
			googleAuth.SetBinding(CoreButton.CommandProperty, "GoogleAuth");


			var facebookAuth = new CoreButton()
			{
				Style = AppStyles.LightOrange,
				Text = "FaceBook Authentication",
				AutomationId = "facebookAuth"
			};
			facebookAuth.SetBinding(CoreButton.CommandProperty, "FaceBookAuth");

			var msAuth = new CoreButton()
			{
				Style = AppStyles.LightOrange,
				Text = "Microsoft Authentication",
				AutomationId = "msAuth"
			};
			msAuth.SetBinding(CoreButton.CommandProperty, "MicrosoftAuth");

			var lblToken = new Label();
			lblToken.SetBinding(Label.TextProperty, "AccessToken");

            var stack = new CompressedStackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { googleAuth, facebookAuth, msAuth, lblToken,new StackLayout() { HeightRequest = 5 } }
            };

            Content = new ScrollView()
			{
				Content = stack
			};
        }
        protected override void OnAppearing()
        {
            this.SetAnalyticsTimeStamp();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {

            this.SaveAnalyticsDetails();
            base.OnDisappearing();
        }
    }
}
