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
                Style = AppStyles.LightOrange,
                AutomationId = "removeEmptyRowsEffect",
                Command = new Command( (obj) =>
                {
                    NavigateTo(new ListViewEffect());
                })
            };

			var hideListSeparatorEffect = new CoreButton()
			{
				Text = "Hide List Separator",
				Style = AppStyles.LightOrange,
				AutomationId = "hideListSeparatorEffect",
				Command = new Command((obj) =>
				{
                    NavigateTo(new ListViewEffect(true));
				})
			};

			var disableWebViewScrollEffect = new CoreButton()
			{
				Text = "WebView Scroll Disable",
				Style = AppStyles.LightOrange,
				AutomationId = "disableWebViewScrollEffect",
				Command = new Command((obj) =>
				{
                    NavigateTo<WebViewEffect>();
				})
			};

			var stack = new StackLayout()
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
	}
}
