using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
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
		
        }
        protected override void OnBindingContextChanged()
        {
            var str = (string)this.BindingContext;
            lbl.Text = str;
            base.OnBindingContextChanged();
        }
    }

    public class ContextMenuPage : ContentPage
    {
        public ContextMenuPage()
        {
            var names = new string[]{
                "Jack Sparrow",
                "Jill Reed",
                "Max Headroom",
                "Bob Smith",
                "Will Marks",
                "Monty Quimby",
                "Adam Williams"
            };

            var lst = new ListView()
            {
                ItemTemplate = new DataTemplate(typeof(ContextMenuViewCell)),
                ItemsSource = names
            };

            Content = new CompressedStackLayout()
            {
                Children = { lst }
            };
        }
    }
}
