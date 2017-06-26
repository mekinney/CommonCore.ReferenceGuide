using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.CommonCore;

/*
    Intercepting/Catching/Detecting [redirect] url change 
    App Linking / Deep linking - custom url schemes
*/
namespace referenceguide.Droid
{
    [Activity(Label = "ActivityCustomUrlSchemeInterceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [
        IntentFilter
        (
            actions: new[] { Android.Content.Intent.ActionView },
            Categories = new[]
                    {
                        Android.Content.Intent.CategoryDefault,
                        Android.Content.Intent.CategoryBrowsable
                    },
            DataSchemes = new[]
                    {
                        "com.googleusercontent.apps.638673396107-ls57h8813oiob1fl1frcifni9p1pd7pf",
                        "fb1483295131691906://localhost/path",
                    },
            //DataHost = "auth",
            DataPath = "/oauth2redirect"
        )
    ]
    public class ActivityCustomUrlSchemeInterceptor : Activity
    {
        string message;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Android.Net.Uri uri_android = Intent.Data;

            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(uri_android.ToString());

            // load redirect_url Page
            AuthenticationState.Authenticator.OnPageLoading(uri_netfx);

            this.Finish();

            return;
        }
    }
}
