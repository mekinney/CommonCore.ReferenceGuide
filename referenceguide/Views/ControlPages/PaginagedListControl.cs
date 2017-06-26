using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class PaginagedListControl: BoundPage<PaginatedViewModel>
    {
        public PaginagedListControl()
        {
            
			var lstView = new ListControl(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(RandomUserCell)),
				AutomationId = "lstView"
			};
			lstView.SetBinding(ListControl.ItemsSourceProperty, "RandomUsers");
            lstView.SetBinding(ListControl.LoadMoreCommandProperty, "LoadMore");
            lstView.SetBinding(ListControl.SelectedItemProperty, "SelectedUser");

			Content = new StackLayout()
			{
				Children = { lstView }
			};
        }
    }
}
