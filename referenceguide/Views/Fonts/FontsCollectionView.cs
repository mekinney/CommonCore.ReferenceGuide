using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Linq;

namespace referenceguide
{
    public class FontView : ContentView
    {
        public FontView()
        {
            var imgLabel = new Label()
            {
                FontSize = 32,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            imgLabel.SetBinding(Label.TextProperty,"Unicode");
            imgLabel.SetBinding(Label.FontFamilyProperty, "FontFamily");


            var descript = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 10,
            };
            descript.SetBinding(Label.TextProperty, "FriendlyName");

            Content = new StackLayout()
            {
                Spacing = 5,
                Children = { imgLabel, descript }
            };
        }
    }

    public class FontsCollectionViewCell: ViewCell
    {
        private FontView col1;
        private FontView col2;
        private FontView col3;

        public FontsCollectionViewCell()
        {
            Height = 75;
            var gd = new Grid();
            gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1,GridUnitType.Star)});
            gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gd.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            col1 = new FontView() { VerticalOptions = LayoutOptions.Center };
            col2 = new FontView(){ VerticalOptions = LayoutOptions.Center };
            col3 = new FontView(){ VerticalOptions = LayoutOptions.Center };

            gd.AddChild(col1, 0, 0);
            gd.AddChild(col2, 0, 1);
            gd.AddChild(col3, 0, 2);

            View = gd;
        }

        protected override void OnBindingContextChanged()
        {
            var binding = (FontItemRow)this.BindingContext;
            col1.BindingContext = binding.Item1;
            col2.BindingContext = binding.Item2;
            col3.BindingContext = binding.Item3;
            base.OnBindingContextChanged();
        }
    }

    public class FontsCollectionView:BoundPage<DataExampleViewModel>
    {
        public FontsCollectionView()
        {
            this.Title = VM.FontType.ToString();
            var list = new CoreListView()
            {
                ItemTemplate = new DataTemplate(typeof(FontsCollectionViewCell)),
                RowHeight = 75,
                SeparatorColor = Color.Transparent,
                SeparatorVisibility =  SeparatorVisibility.None
            };
            list.SetBinding(CoreListView.ItemsSourceProperty,"Items");

            Content = list;
        }

    }
}
