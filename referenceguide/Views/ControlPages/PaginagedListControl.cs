using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class PaginagedListControl: BoundPage<PaginatedViewModel>
    {
        public PaginagedListControl()
        {
            
			var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(RandomUserCell)),
				AutomationId = "lstView"
			};
			lstView.SetBinding(CoreListView.ItemsSourceProperty, "RandomUsers");
            lstView.SetBinding(CoreListView.LoadMoreCommandProperty, "LoadMore");
            lstView.SetBinding(CoreListView.SelectedItemProperty, "SelectedUser");

			Content = new StackLayout()
			{
				Children = { lstView }
			};
        }
    }
}
