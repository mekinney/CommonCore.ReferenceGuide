using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using Android.Widget;
using Xamarin.Forms.CommonCore;
using System;
using Microsoft.Identity.Client;

namespace referenceguide.Droid
{

    [Activity(Label = "referenceguide.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            AppData.Instance.SearchView = Resource.Id.searchView;

			base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0)); //here


        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}


        protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}
