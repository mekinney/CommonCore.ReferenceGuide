using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace referenceguide.Droid
{
    [Activity(Label = "CommonCore", 
              MainLauncher = true, 
              NoHistory = true, 
              Theme = "@style/Theme.Splash",
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [MetaData("android.app.shortcuts", Resource = "@xml/shortcuts")]
	public class SplashScreenActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);

		}
	}
}
