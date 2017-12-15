using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class ViewModel1: CoreViewModel
    {
        public string Content { get; set; } = "This is content from view model 1 and unrelated to another binding class";
        public Color TextColor { get; set; } = Color.Black;
        public ICommand ChangeColor { get; set; }
        public ViewModel1()
        {
            ChangeColor = new CoreCommand((obj) => {
                TextColor = Color.Orange;
            });
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            
        }
    }
    public class ViewModel2 : CoreViewModel
    {
        public string Content { get; set; } = "This is content from view model 2 and demonstrates that a page can have multiple bindings";
        public Color TextColor { get; set; } = Color.Black;
        public ICommand ChangeColor { get; set; }
        public ViewModel2()
        {
            ChangeColor = new CoreCommand((obj) => {
                TextColor = Color.Blue;
            });
        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            
        }
    }

    public class BindingViewMain: BasePages
    {
        public BindingViewMain()
        {
            this.Title = "Multiple View Models";

            var lbl1 = new Label();
            lbl1.SetBinding(Label.TextProperty, "Content");
            lbl1.SetBinding(Label.TextColorProperty, "TextColor");
            var btn1 = new Button() { Text = "Change Color" };
            btn1.SetBinding(Button.CommandProperty,"ChangeColor");

            var lbl2 = new Label();
            lbl2.SetBinding(Label.TextProperty, "Content");
            lbl2.SetBinding(Label.TextColorProperty, "TextColor");
            var btn2 = new Button() { Text = "Change Color" };
            btn2.SetBinding(Button.CommandProperty, "ChangeColor");

            //All objects in this view1 are bound to ViewModel 1
            var view1 = new CoreContenView<ViewModel1>()
            {
                Content = new StackLayout()
                {
                    Children = { lbl1, btn1 }
                }
            };

            //All objects in this view1 are bound to ViewModel 2
            var view2 = new CoreContenView<ViewModel2>()
            {
                Content = new StackLayout()
                {
                    Children = { lbl2, btn2 }
                }
            };


            Content = new CompressedStackLayout()
            {
                Padding=10,
                Spacing=10,
                Children = { view1, view2 }
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
