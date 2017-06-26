using System;
using System.Collections.Generic;
using System.Net;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class MainPage : MasterDetailPage
	{
		public static Page CurrentDetail { get; set; }
		public MainPage()
		{
			try
			{
                Master = new SlidingPage();
				Detail = new NavigationPage(new LandingPage())
				{
					BarBackgroundColor = Color.FromHex("#b85921"),
					BarTextColor = Color.White
				};
				AppData.Instance.AppNav = Detail.Navigation;
				MainPage.CurrentDetail = Detail;
			}
			catch (Exception ex)
			{
				var x = ex.Message;
			}

		}
	}
}
