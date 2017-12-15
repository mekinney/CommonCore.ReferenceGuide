using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class SlidingPageCell : ViewCell
	{
		private readonly CachedImage img;
		private readonly Label lbl;

		public SlidingPageCell()
		{

			img = new CachedImage()
			{
				Margin = new Thickness(10, 0, 3, 5),
				HeightRequest = 22,
				WidthRequest = 22,
				DownsampleHeight = 22,
				DownsampleWidth = 22,
				Aspect = Aspect.AspectFit,
				CacheDuration = TimeSpan.FromDays(30),
				VerticalOptions = LayoutOptions.Center,
				DownsampleUseDipUnits = true
			};

			lbl = new Label()
			{
				Margin = 5,
				VerticalOptions = LayoutOptions.Center,
			};

			View = new CompressedStackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children = { img, lbl }
			};
		}

		//On a listview that uses RecycleElement binding can be costly
		protected override void OnBindingContextChanged()
		{
			var item = (SlidingPageItem)BindingContext;
			img.Source = item.IconSource;
			lbl.Text = item.Title;

			base.OnBindingContextChanged();
		}
	}

	public class SlidingPage : CorePage<MasterDetailViewModel>
	{

		public SlidingPage()
		{
			BackgroundColor = Color.FromHex("#b85921");
			
			var monkey = new CachedImage()
			{
				Margin = 5,
				Source = "iconwhite.png"
			};
			var navTitle = new Label()
			{
				Text = "Common Core",
				TextColor = Color.White,
				Margin = 5
			};
			var navSubtitle = new Label()
			{
				Text = "Options Menu",
				TextColor = Color.White,
				Style = CoreStyles.AddressCell
			};

			var topPanel = new StackLayout()
			{
				Padding = new Thickness(10, 0, 10, 10),
				BackgroundColor = Color.FromHex("#b85921"),
				Orientation = StackOrientation.Horizontal,
				Children = { monkey, new StackLayout() { Children = { navTitle, navSubtitle } } }
			};

			var listView = new CoreListView
			{
				BackgroundColor = Color.White,
				ItemTemplate = new DataTemplate(typeof(SlidingPageCell)),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None,
			};
            listView.SetBinding(CoreListView.ItemsSourceProperty,"MasterPageItems");
            listView.SetBinding(CoreListView.ItemClickCommandProperty,"NavClicked");

			Padding = new Thickness(0, 40, 0, 0);
			Icon = "hamburger.png";
			Title = "Reference Guide";
            Content = new CompressedStackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					topPanel,
					listView
				}
			};

		}
	}
}
