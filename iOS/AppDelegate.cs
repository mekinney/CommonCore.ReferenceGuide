using System;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Touch;
using Foundation;
using Microsoft.Identity.Client;
using PushNotification.Plugin;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

#if DEBUG
            AppBuid.CurrentBuid = "dev";
#elif QA
            AppBuid.CurrentBuid = "qa";
#elif RELEASE
			AppBuid.CurrentBuid = "prod";
#endif

            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

            CrossPushNotification.Initialize<CrossPushNotificationListener>();
            CrossPushNotification.Current.Register();

            LoadApplication(new App());

            DependencyService.Get<ILocalNotify>().RequestPermission((permit) => { });

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            AppData.Instance.DeviceToken = deviceToken;
            if (CrossPushNotification.Current is IPushNotificationHandler)
            {
                ((IPushNotificationHandler)CrossPushNotification.Current).OnRegisteredSuccess(deviceToken);
            }

        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            application.RegisterForRemoteNotifications();
        }

        // Uncomment if using remote background notifications. To support this background mode, enable the Remote notifications option from the Background modes section of iOS project properties. (You can also enable this support by including the UIBackgroundModes key with the remote-notification value in your app’s Info.plist file.)
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            if (CrossPushNotification.Current is IPushNotificationHandler)
            {
                ((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
            }

        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            if (CrossPushNotification.Current is IPushNotificationHandler)
            {
                ((IPushNotificationHandler)CrossPushNotification.Current).OnMessageReceived(userInfo);
            }
        }


        public override bool OpenUrl(UIApplication application,NSUrl url,string sourceApplication,NSObject annotation)
        {
            var absoluteUrl = url.AbsoluteString;
            var msurl = $"msal{AppData.Instance.MicrosoftAppId}://auth";

            if (absoluteUrl.StartsWith(msurl, StringComparison.CurrentCultureIgnoreCase))
            {
                AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            }
            else{
				Uri uri_netfx = new Uri(url.AbsoluteString);
				AuthenticationState.Authenticator.OnPageLoading(uri_netfx);
            }

            return true;
        }
    }


}
