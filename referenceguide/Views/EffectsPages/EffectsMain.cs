using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class EffectsMain : BasePages
	{
		public EffectsMain()
		{
			this.Title = "Effects";

			var removeEmptyRowsEffect = new CoreButton()
            {
                Text = "Remove Empty Cell",
                Style = CoreStyles.LightOrange,
                AutomationId = "removeEmptyRowsEffect",
                Command = new Command( (obj) =>
                {
                    Navigation.PushNonAwaited(new ListViewEffect());
                })
            };

			var hideListSeparatorEffect = new CoreButton()
			{
				Text = "Hide List Separator",
				Style = CoreStyles.LightOrange,
				AutomationId = "hideListSeparatorEffect",
				Command = new Command((obj) =>
				{
                    Navigation.PushNonAwaited(new ListViewEffect(true));
				})
			};

			var disableWebViewScrollEffect = new CoreButton()
			{
				Text = "WebView Scroll Disable",
				Style = CoreStyles.LightOrange,
				AutomationId = "disableWebViewScrollEffect",
				Command = new Command((obj) =>
				{
                    Navigation.PushNonAwaited<WebViewEffect>();
				})
			};

            var stack = new CompressedStackLayout()
			{
				Padding = 20,
				Spacing = 10,
				Children = { removeEmptyRowsEffect, hideListSeparatorEffect, disableWebViewScrollEffect,new StackLayout() { HeightRequest = 5 } }
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
