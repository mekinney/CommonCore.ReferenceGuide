using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms.CommonCore;

namespace referenceguide.Droid
{
	[Activity(Label = "CommonCore", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash",
	ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class SplashScreenActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

			LocalNotify.MainType = typeof(MainActivity);
			AppData.Instance.AppIcon = Resource.Drawable.icon;

			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);

		}
	}
}
