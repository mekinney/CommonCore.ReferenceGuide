using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class ErrorsPageCell:ViewCell
    {
		public ErrorsPageCell()
		{
            var lbl = new Label()
            {
                Margin = 5
            };
            lbl.SetBinding(Label.TextProperty, "ErrorType");

            View = new StackLayout()
            {
                Children = { lbl }
            };
		}
    }

    public class ErrorsPage : BoundPage<LoggingViewModel>
    {
        public ErrorsPage()
        {
            this.Title = "App Errors";

            var lst = new ListControl()
            {
                ItemTemplate = new DataTemplate(typeof(ErrorsPageCell)),
                ItemClickCommand= new Command((obj) => {
                    var log = (ErrorLog)obj;
                    VM.DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = log.ErrorMessage
                    });
                })
            };
            lst.SetBinding(ListControl.ItemsSourceProperty, "ErrorLogs");

			var btnAdd = new GradientButton()
			{
				Text = "Add",
                Style = AppStyles.LightOrange,
                AutomationId = "btnAdd",
			};
			btnAdd.SetBinding(GradientButton.CommandProperty, "CreateErrorEntry");

            var btnClear = new GradientButton()
            {
                Text = "Clear",
                Style = AppStyles.LightOrange,
                AutomationId = "btnClear"
            };
            btnClear.SetBinding(GradientButton.CommandProperty,"ClearErrorEntries");
           
            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { lst ,btnAdd, btnClear }
            };
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();
			VM.LoadResources();
		}
    }
}
