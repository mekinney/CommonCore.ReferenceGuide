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

			CoreSettings.NotificationTags.Add("referenceguide");
			MainPage = new MainPage();
		}

		private void ConnectivityChanged(object sender, ConnectivityChangedEventArgs args)
		{
            this.SetConnectionStatus(args.IsConnected);
		}
        private void AppScreenSizeChanged(object sender, EventArgs args)
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
        }

		protected override void OnStart()
		{
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
            MainPage.SizeChanged += AppScreenSizeChanged;
			CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
		}

		protected override void OnSleep()
		{
            MainPage.SizeChanged -= AppScreenSizeChanged;
			CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
		}

		protected override void OnResume()
		{
			MainPage.SizeChanged += AppScreenSizeChanged;
			CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
		}

        private void InitCustomRenders()
        {
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

        }

	}
}
