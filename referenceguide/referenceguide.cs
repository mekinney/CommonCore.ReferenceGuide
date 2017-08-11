using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using PushNotification.Plugin;
using System.Json;

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

		protected override void OnStart()
		{
			//var mobileCenterKeys = $"android={AppData.Instance.MobileCenter_HockeyAppAndroid};ios={AppData.Instance.MobileCenter_HockeyAppiOS}";
			//MobileCenter.Start(mobileCenterKeys, typeof(Analytics), typeof(Crashes));
			
			CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
		}

		protected override void OnSleep()
		{
			CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
		}

		protected override void OnResume()
		{
			CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
		}
	}
}
