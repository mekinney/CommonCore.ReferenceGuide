using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{

    public class HttpServicesPage : BoundPage<DataExampleViewModel>
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

            var lstView = new ListControl(ListViewCachingStrategy.RecycleElement)
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(RandomUserCell)),
                AutomationId="lstView"
            };
            lstView.SetBinding(ListControl.ItemsSourceProperty, "RandomUsers");


            Content = new StackLayout()
            {
                Children = { lstView }
            };
        }

    }
}
