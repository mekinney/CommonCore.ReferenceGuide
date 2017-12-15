using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class ServicesMain : CorePage<DataExampleViewModel>
    {
        public ServicesMain()
        {
            this.Title = "Services";

            var http = new CoreButton()
            {
                Text = "Http Services",
                Style = CoreStyles.LightOrange,
                AutomationId = "http",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PushAsync(new HttpServicesPage());
                })
            };

			var httpPost = new CoreButton()
			{
				Text = "Http Post",
				Style = CoreStyles.LightOrange,
				AutomationId = "httpPost",
			};
            httpPost.SetBinding(CoreButton.CommandProperty,"HttpPost");

			var httpDownload = new CoreButton()
			{
				Text = "Http Download",
				Style = CoreStyles.LightOrange,
				AutomationId = "httpDownload",
			};
			httpDownload.SetBinding(CoreButton.CommandProperty, "LongDownload");

            var sqlite = new CoreButton()
            {
                Text = "Sqlite",
                Style = CoreStyles.LightOrange,
                AutomationId = "sqlite",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PushAsync(new SqlitePage());
                })
            };

            var encrypt = new CoreButton()
            {
                Text = "Encryption",
                Style = CoreStyles.LightOrange,
                AutomationId = "encrypt",
                Command = new Command(async (obj) =>
                {
                    await Navigation.PushAsync(new EncryptionPage());
                })
            };

			var analytics = new CoreButton()
			{
				Text = "Analytics",
				Style = CoreStyles.LightOrange,
				AutomationId = "analytics",
				Command = new Command(async (obj) =>
				{
					await Navigation.PushAsync(new AnalyticsPage());
				})
			};

			var errors = new CoreButton()
			{
				Text = "Errors",
				Style = CoreStyles.LightOrange,
				AutomationId = "errors",
				Command = new Command(async (obj) =>
				{
					await Navigation.PushAsync(new ErrorsPage());
				})
			};

			var timerSrv = new CoreButton()
			{
				Text = "Background Timer",
				Style = CoreStyles.LightOrange,
				AutomationId = "timerSrv",
			};
            timerSrv.SetBinding(CoreButton.TextProperty,"BackgroundButtonTitle");
            timerSrv.SetBinding(CoreButton.CommandProperty,"StartBackgrounding");

            var stack = new CompressedStackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { http, httpPost, httpDownload, sqlite, encrypt, analytics, errors, timerSrv, new StackLayout() { HeightRequest = 5 } }
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
