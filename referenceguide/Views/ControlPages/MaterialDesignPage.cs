using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms.CommonCore.MaterialDesign;

namespace referenceguide
{
	public class MaterialDesignPage : CoreAbsoluteLayoutPage<SimpleViewModel>
	{

		public MaterialDesignPage()
		{
			this.Title = "Material Design";

			var fab = new CoreFloatingActionButton()
			{
				Size = FABControlSize.Normal,
				ColorNormal = Color.FromHex("#DF8049"),
				ColorPressed = Color.FromHex("#DF8049").MultiplyAlpha(0.4),
				ImageName = "plus.png"
			};

			fab.SetBinding(CoreFloatingActionButton.CommandProperty, "FABClicked");
			AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
			AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(0.95f, 0.95f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));


			var fteUserName = new CoreFloatingTextEntry()
			{
				Placeholder = "User Name",
				ErrorText = "Required Field",
				ErrorColor = Color.Red,
				Validator = CoreFloatingTextEntry.RequiredValidator
			};

			var ftePassword = new CoreFloatingTextEntry()
			{
				Placeholder = "Password",
				IsPassword = true,
				ErrorText = "Required Field",
				ErrorColor = Color.Red,
				Validator = CoreFloatingTextEntry.RequiredValidator
			};


            Content = new CompressedStackLayout()
			{
				Padding = new Thickness(20, 30, 20, 20),
				Spacing = Device.OnPlatform<double>(40,5,5),
				Children = { fteUserName, ftePassword }
			};

			AbsoluteLayer.Children.Add(fab);
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
