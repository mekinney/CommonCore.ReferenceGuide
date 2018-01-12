using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Linq;

namespace referenceguide
{
    public class ContextMenuViewModel : CoreViewModel
    {
        public ObservableCollection<string> Names { get; set; }

        public override void OnViewMessageReceived(string key, object obj)
        {
            switch(key){
                case CoreSettings.LoadResources:
                    var names = new string[]{
                        "Jack Sparrow",
                        "Jill Reed",
                        "Max Headroom",
                        "Bob Smith",
                        "Will Marks",
                        "Monty Quimby",
                        "Adam Williams"
                    };
                    Names = names.ToObservable<string>();
                    break;
                case "NameDeleted":
                    if (obj != null)
                    {
                        var item = Names.FirstOrDefault(x => x.Equals((string)obj));
                        if(item!=null)
                            Names.Remove(item);
                    }
                    break;
            }
        }
    }
    public class ContextMenuViewCell : ViewCell
    {
        Label lbl;
        Action add;
        Action edit;
        Action delete;

        public ContextMenuViewCell()
        {
            lbl = new Label() { Margin = 10 };
            add = Add;
            edit = Edit;
            delete = Delete;

            if (Device.RuntimePlatform.ToUpper() == "IOS")
            {
                this.ContextActions.Add(new MenuItem()
                {
                    Text = "Add",
                    Command = new Command(add)
                });
				this.ContextActions.Add(new MenuItem()
				{
					Text = "Edit",
					Command = new Command(edit)
				});
                this.ContextActions.Add(new MenuItem()
                {
                    Text = "Delete",
                    IsDestructive = true,
                    Command = new Command(delete)
                });
            }
            else
            {
                this.Tapped += (e, a) =>
                {
                    var dict = new Dictionary<string, Action>();
                    dict.Add("Add", add);
                    dict.Add("Edit", edit);
                    dict.Add("Delete", delete);
                    DependencyService.Get<IContextMenuService>().ShowContextMenu(this.View, dict);
                };
            }


            View = new CompressedStackLayout()
            {
                Children = { lbl }
            };
        }
        private void Add(){
      
        }
        private void Edit(){
			
        }
        private void Delete(){
            var str = (string)this.BindingContext;
            CoreDependencyService.GetViewModel<ContextMenuViewModel>().OnViewMessageReceived("NameDeleted", str);
        }
        protected override void OnBindingContextChanged()
        {
            var str = (string)this.BindingContext;
            lbl.Text = str;
            base.OnBindingContextChanged();
        }
    }

    public class ContextMenuPage : CorePage<ContextMenuViewModel>
    {
        public ContextMenuPage()
        {
            var lst = new ListView()
            {
                ItemTemplate = new DataTemplate(typeof(ContextMenuViewCell)),
            };
            lst.SetBinding(ListView.ItemsSourceProperty,"Names");

            Content = new CompressedStackLayout()
            {
                Children = { lst }
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
