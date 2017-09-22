using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class PaginagedListControl: BoundPage<DataExampleViewModel>
    {
        public PaginagedListControl()
        {
            
			var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
			{
				HasUnevenRows = true,
				ItemTemplate = new DataTemplate(typeof(RandomUserCell)),
				AutomationId = "lstView"
			};
			lstView.SetBinding(CoreListView.ItemsSourceProperty, "PaginatedRandomUsers");
            lstView.SetBinding(CoreListView.LoadMoreCommandProperty, "LoadMorePaginatedUsers");
            lstView.SetBinding(CoreListView.SelectedItemProperty, "SelectedPagingatedUser");

			Content = new StackLayout()
			{
				Children = { lstView }
			};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.GetPaginatedRandomUsers().ContinueOn();
        }
    }
}
