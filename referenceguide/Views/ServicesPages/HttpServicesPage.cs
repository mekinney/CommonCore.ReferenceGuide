using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{

    public class HttpServicesPage : CorePage<DataExampleViewModel>
    {
        public HttpServicesPage()
        {
            this.Title = "Http Services";

            this.ToolbarItems.Add(new ToolbarItem("Load", null, () =>
            {
                VM.HttpDownloadStart.Execute(null);
            }){ 
                AutomationId="Load"
            });

            var lstView = new CoreListView(ListViewCachingStrategy.RecycleElement)
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(RandomUserCell)),
                AutomationId="lstView"
            };
            lstView.SetBinding(CoreListView.ItemsSourceProperty, "RandomUsers");


            Content = new CompressedStackLayout()
            {
                Children = { lstView }
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
