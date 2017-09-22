using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AnalyticsPageCell:ViewCell
    {
        public AnalyticsPageCell()
        {
			var lbl = new Label()
			{
				Margin = 5
			};
			lbl.SetBinding(Label.TextProperty, "ViewName");

			View = new StackLayout()
			{
				Children = { lbl }
			};

        }
    }

    public class AnalyticsPage :BoundPage<DataExampleViewModel>
    {
     
        public AnalyticsPage()
        {
			this.Title = "App Analytics";

			var lst = new CoreListView()
			{
				ItemTemplate = new DataTemplate(typeof(AnalyticsPageCell))
			};
			lst.SetBinding(CoreListView.ItemsSourceProperty, "AnalyticLogs");

			var btnClear = new CoreButton()
			{
				Text = "Clear",
				Style = AppStyles.LightOrange,
				AutomationId = "btnClear"
			};
			btnClear.SetBinding(CoreButton.CommandProperty, "ClearAnalyticEntries");

			Content = new StackLayout()
			{
                Padding = 20,
                Spacing = 10,
				Children = { lst, btnClear }
			};
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			VM.LoadResources();
		}

    }
}
