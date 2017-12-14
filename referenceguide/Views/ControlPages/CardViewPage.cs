using System;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CardViewCell:ViewCell
    {
        public CardViewCell()
        {
            var baseHeight = (int)(AppSettings.ScreenSize.Height * 0.4);
            var footerHeight = AppSettings.OnPlatform<int>(20, 30);
            var lblMaring = AppSettings.OnPlatform<int>(5, 10);

            this.Height = baseHeight + footerHeight + 10;

            var img = new CachedImage()
            {
                HeightRequest = baseHeight - footerHeight,
                Aspect = Aspect.AspectFill,
                DownsampleHeight = baseHeight - footerHeight,
            };
            img.SetBinding(CachedImage.SourceProperty, "ImageUrl");

            var lbl = new Label()
            {
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(lblMaring,lblMaring,0,0),
            };
            lbl.SetBinding(Label.TextProperty,"Title");


            var lblStars = new Label()
            {
                Margin=new Thickness(0, lblMaring, lblMaring, 0),
                TextColor = Color.Red,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.End,
            };
            lblStars.SetBinding(Label.FontFamilyProperty, "FontFamily");
            lblStars.SetBinding(Label.TextProperty, "CountText");

            var panel = new StackLayout()
            {
                HeightRequest = footerHeight,
                BackgroundColor = Color.FromHex("#f6f6f6"),
                Orientation = StackOrientation.Horizontal,
                Children = { lbl, lblStars }
            };

            var cv = new CoreCardView()
            {
                Margin=5,
                HeightRequest = baseHeight + footerHeight,
                CornerRadius = 5,
                BackgroundColor = Color.FromHex("#f6f6f6"),
                Content = new StackLayout()
                {
                    Children =
                    {
                        img, panel
                    }
                }
            };

            View = new StackLayout()
            {
                Children = { cv }
            };
        }
    }
    public class CardViewPage :CorePage<SimpleViewModel>
    {
        public CardViewPage()
        {
            this.Title = "Card View";
            var lst = new CoreListView()
            {
                RowHeight = (int)(AppSettings.ScreenSize.Height * 0.4) + AppSettings.OnPlatform<int>(35,45),
                ItemTemplate = new DataTemplate(typeof(CardViewCell)),
                SeparatorVisibility = SeparatorVisibility.None,
                SeparatorColor = Color.Transparent
            };
            lst.SetBinding(CoreListView.ItemsSourceProperty, "CardCollection");
            lst.Effects.Add(new HideListSeparatorEffect());


            Content = new StackLayout()
            {
                Padding = 10,
                Children = { lst }
            };
        }
    }
}
