using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class StateViewCell:ViewCell
    {
        public StateViewCell()
        {
            var lbl = new Label() { Margin = 5 };
            lbl.SetBinding(Label.TextProperty,"Text");
            View = new StackLayout()
            {
                Children = { lbl }
            };
        }
    }
    public class StateView: PopupView 
    {
        public StateView()
        {
            //this.AnimateOpen = false;
            var lbl = new Label() { Text = "Select A State", Margin = 10 };
            var top = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#DF8049"),
                Children = { lbl }
            };

            var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                ItemTemplate = new DataTemplate(typeof(StateViewCell)),
                MaintainSelection=true
            };
            lstView.SetBinding(CoreListView.ItemsSourceProperty,"States");

            var placeholder = new StackLayout() { 
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
			var btn = new Button()
			{
                Margin=5,
				Text = "Close",
                BackgroundColor = Color.Transparent,
				Command = new Command(() =>
				{
					this.Close();
				})
			};

            var bottom = new StackLayout()
            { 
                Orientation= StackOrientation.Horizontal,
                Children = { placeholder, btn }
            };



            Content = new StackLayout()
            {
                Children = { top, lstView, bottom }
            };
        }


    }
    public class ControlsMain : AbsoluteLayoutPage<SimpleViewModel>
    {

        public ControlsMain()
        {
            this.Title = "Controls";

            var md = new CoreButton()
            {
                Text = "Material Design",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PushAsync(new MaterialDesignPage());
                })
            };

            var listPage = new CoreButton()
            {
                Text = "List Control",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PushAsync(new PaginagedListControl());
                })
            };


            var searchPage = new CoreButton()
            {
                Text = "Search Page",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
               {
                   await AppSettings.AppNav.PushAsync(new SearchContentPage());
               })
            };

            var popup = new CoreButton()
            {
                Text = "Popup Control",
                Style = AppStyles.LightOrange,
                Command = new Command((obj) =>
               {
                   this.ShowPopup(new StateView(), new Rectangle(0.5, 0.5, 0.85, 0.5), 5);
               })
            };

			var carouselPage = new CoreButton()
			{
				Text = "Carousel Page",
				Style = AppStyles.LightOrange,
				Command = new Command(async (obj) =>
			   {
				   await AppSettings.AppNav.PushAsync(new CarouselMain());
			   })
			};

			var bckImage = new CoreButton()
			{
				Text = "Background Image",
				Style = AppStyles.LightOrange,
				Command = new Command(async(obj) =>
			   {
				   await AppSettings.AppNav.PushAsync(new BackgroundImagePage());
			   })
			};

            var pnl = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Children ={
                    new Label(){
                        Margin=5,
                        Text="Toggle A Selection",
                        HorizontalOptions= LayoutOptions.StartAndExpand
                    },
                    new CoreSwitch(){
                        TrueColor=Color.FromHex("#DF8049")
                    }
                }
            };

            var ranking = new CoreRanking()
            {
                Margin = 5,
                Count = 5,
                UnSelectedImage = "star_outline.png",
                SelectedImage = "star_selected.png",
            };
            ranking.SetBinding(CoreRanking.SelectedRankProperty, new Binding("SelectedRank", BindingMode.TwoWay));

            var starLbl = new Label()
            {
                Margin = 5,
                HorizontalOptions = LayoutOptions.Center
            };
            starLbl.SetBinding(Label.TextProperty, "SelectedRankText");



            var x = Font.Default.WithSize(10);

            var radioGroup = new CoreRadioGroup()
            {
                Spacing=15,
                SelectedImage = "checked.png",
                UnSelectedImage = "unchecked.png",
                CheckedCommand = new RelayCommand((obj) => {
                    
                    var z = obj;
                })

            };
            radioGroup.SetBinding(CoreRadioGroup.ItemsSourceProperty, "RadioOptions");
            radioGroup.SetBinding(CoreRadioGroup.SelectedIndexProperty, "SelectedRadioIndex");

            var stack = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { listPage, md, searchPage, popup, carouselPage, bckImage, pnl, ranking, starLbl, radioGroup }
            };

            Content = new ScrollView()
            {
                Content = stack
            };

        }
    }
}
