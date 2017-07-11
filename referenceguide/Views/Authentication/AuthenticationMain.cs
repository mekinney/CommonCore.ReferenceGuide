using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AuthenticationMain :BoundPage<AuthenticationViewModel>
    {
        public AuthenticationMain()
        {
            this.Title = "Authentications";

			var googleAuth = new GradientButton()
			{
				Style = AppStyles.LightOrange,
				Text = "Google Authentication",
				AutomationId = "googleAuth"
			};
			googleAuth.SetBinding(GradientButton.CommandProperty, "GoogleAuth");


			var facebookAuth = new GradientButton()
			{
				Style = AppStyles.LightOrange,
				Text = "FaceBook Authentication",
				AutomationId = "facebookAuth"
			};
			facebookAuth.SetBinding(GradientButton.CommandProperty, "FaceBookAuth");

			var msAuth = new GradientButton()
			{
				Style = AppStyles.LightOrange,
				Text = "Microsoft Authentication",
				AutomationId = "msAuth"
			};
			msAuth.SetBinding(GradientButton.CommandProperty, "MicrosoftAuth");

			var lblToken = new Label();
			lblToken.SetBinding(Label.TextProperty, "AccessToken");

            var stack = new StackLayout()
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
    }
}
