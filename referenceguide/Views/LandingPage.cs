using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class LandingPage : ContentPage
	{
		public LandingPage()
		{
			Title = "Landing";
			BackgroundColor = Color.White;

			var topImage = new CachedImage()
			{
				Source = "sharedcode.png"
			};

			var customFont = new Label()
			{
				Style = AppStyles.FontLabel,
				Text = "Xamarin.Forms.CommonCore"
			};

			var fs = new FormattedString();
			
            //using an extension method to add span with text
            fs.AddTextSpan("  Xamarin.Forms provides a platform to resuse code across logic and UI development. " +
					"There is tremendous debate on the use of portable class libraries versus shared projects. \n\n");
			
            fs.AddTextSpan("  After using the CommonCore project, the benefits of shared projects should be apparent with nested files, compiler directives and ease of change." +
					" It is still possible to write spaghetti code which XAML does help prevent but good team standards can mitigate these issues.\n\n");

            fs.AddTextSpan("  CommonCore uses Unity to create static instances of the application's view models and service classes through dependency injection." +
                           " Take a moment to view the readme file in order to understand all the nuget files and configuration settings available through CommonCore.");


			var lbl = new Label() { FormattedText = fs, Margin = 10 };


            var pnl = new StackLayout()
            {
                Children = { topImage, 
                    customFont,
                    lbl }
            };

			Content = new ScrollView()
			{
				Content = pnl
			};

			if (CoreSettings.AppData.Instance.Settings.AnalyticsEnabled)
			{
				InjectionManager.GetViewModel<SimpleViewModel>().Log.LogAnalytics(this.GetType().FullName);
			}
		}
	}
}
