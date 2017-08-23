using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms.CommonCore;
using System;

namespace referenceguide
{
    public class App : Application
	{
		public App()
		{
		
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


	}
}
