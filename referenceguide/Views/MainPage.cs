using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class MainPage : CoreMasterDetailPage<MasterDetailViewModel>
	{
		public static Page CurrentDetail { get; set; }
		public MainPage()
		{
            
			try
			{
                Master = new SlidingPage();
				Detail = new Xamarin.Forms.NavigationPage(new LandingPage())
				{
					BarBackgroundColor = Color.FromHex("#b85921"),
					BarTextColor = Color.White
				};
				CoreSettings.AppNav = Detail.Navigation;
				MainPage.CurrentDetail = Detail;

			}
			catch (Exception ex)
			{
				var x = ex.Message;
			}

            this.SetBinding(MasterDetailPage.IsPresentedProperty, new Binding("IsPresented",BindingMode.TwoWay));

		}
	}
}
