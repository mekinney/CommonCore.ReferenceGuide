using System;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class RandomUserCell : ViewCell
	{
		private readonly CachedImage img;
		private readonly Label lblFullName;
		private readonly Label lblFullAddress;

		public RandomUserCell()
		{
			this.Height = 85;
			img = new CachedImage()
			{
				Margin = new Thickness(8, 4, 4, 4),
				HeightRequest = 65,
				WidthRequest = 65,
				RetryCount = 0,
				RetryDelay = 250,
				LoadingPlaceholder = "placeholder.png",
				CacheDuration = TimeSpan.FromDays(10),
				Transformations = new System.Collections.Generic.List<ITransformation>() {
					new CircleTransformation()
				},
             
			};
			img.Effects.Add(new ViewShadowEffect());

			lblFullName = new Label()
			{
				Margin = new Thickness(4, 2, 4, -4)
			};

			lblFullAddress = new Label()
			{
				Style = AppStyles.AddressCell
			};

			var rightPanel = new StackLayout()
			{
				Padding = 0,
				Children = { lblFullName, lblFullAddress }
			};

			ContextActions.Add(new MenuItem()
			{
				Text = "More Info",
				IsDestructive = true,
				Command = new Command((obj) =>
				{
					DependencyService.Get<IDialogPrompt>().ShowMessage(new Prompt()
					{
						Title = "Row Selected",
						Message = $"You chose {((RandomUser)BindingContext).FullName}"
					});
				})
			});

			View = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children = { img, rightPanel }
			};
		}

		//On a listview that uses RecycleElement binding can be costly
		protected override void OnBindingContextChanged()
		{

			var item = ((RandomUser)BindingContext);
			img.Source = item.ImageUrl;
			lblFullName.Text = item.FullName;
			lblFullAddress.FormattedText = item.FullAddress;
			base.OnBindingContextChanged();
		}
	}
}
