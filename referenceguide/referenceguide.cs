using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms.CommonCore;
using System;
using System.Collections.Generic;

#if __ANDROID__
using FFImageLoading.Forms.Droid;
using CarouselView.FormsPlugin.Android;
#else
using FFImageLoading.Forms.Touch;
using CarouselView.FormsPlugin.iOS;
#endif

namespace referenceguide
{


    public class App : Application
	{
		public App()
		{
            InitCustomRenders();

            //if(CoreSettings.OS== DeviceOS.ANDROID){
            //    var lst = new List<ShortDetail>();
            //    lst.Add(new ShortDetail()
            //    {
            //        LongLabel = "Test",
            //        ShortLabel = "Test",
            //        Icon = "icon"
            //    });
            //    lst.Add(new ShortDetail()
            //    {
            //        LongLabel = "Test1",
            //        ShortLabel = "Test1",
            //        Icon = "icon"
            //    });
            //    DependencyService.Get<IAppShortCut>().CreateAppShortCuts("test", lst);
            //}

			AppSettings.NotificationTags.Add("referenceguide");
			MainPage = new MainPage();
		}

		private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
		{
			AppSettings.IsConnected = args.IsConnected;
		}
        private void AppScreenSizeChanged(object sender, EventArgs args)
        {
            AppSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
        }

		protected override void OnStart()
		{
            AppSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
            MainPage.SizeChanged += AppScreenSizeChanged;
			CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
		}

		protected override void OnSleep()
		{
            MainPage.SizeChanged -= AppScreenSizeChanged;
			CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
            this.SaveViewModelState();
		}

		protected override void OnResume()
		{
			MainPage.SizeChanged += AppScreenSizeChanged;
			CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
            this.LoadViewModelState();
		}

        private void InitCustomRenders()
        {
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

        }

	}
}
