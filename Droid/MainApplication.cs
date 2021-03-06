using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Plugin.CurrentActivity;
using PushNotification.Plugin;
using Xamarin.Forms.CommonCore;

namespace referenceguide.Droid
{

    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public static Activity AppContext
        {
            get { return CrossCurrentActivity.Current.Activity; }
        }

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

#if DEBUG
            CoreSettings.CurrentBuid = "dev";
#elif QA
            CoreSettings.CurrentBuid = "qa";
#elif RELEASE
            CoreSettings.CurrentBuid = "prod";
#endif

            RegisterActivityLifecycleCallbacks(this);


            InitGlobalLibraries();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            CoreDependencyService.GetService<ILogService, LogService>().LogException(
                unobservedTaskExceptionEventArgs.Exception,
                "MainApplication -TaskSchedulerOnUnobservedTaskException");

            unobservedTaskExceptionEventArgs.Exception.ConsoleWrite();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var ex = unhandledExceptionEventArgs.ExceptionObject as Exception;
            CoreDependencyService.GetService<ILogService, LogService>().LogException(
                ex,
                "MainApplication -CurrentDomainOnUnhandledException");

            ex.ConsoleWrite();

        }

        public override void OnTerminate()
        {

            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        public static void StartPushService()
        {
            AppContext.StartService(new Intent(AppContext, typeof(PushNotificationService)));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {

                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);

            }
        }

        public static void StopPushService()
        {
            AppContext.StopService(new Intent(AppContext, typeof(PushNotificationService)));
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {
                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);

            }
        }

        private void InitGlobalLibraries()
        {

            CoreSettings.AppIcon = Resource.Drawable.icon;
            CrossPushNotification.Initialize<CrossPushNotificationListener>(CoreSettings.Config.SocialMedia.GoogleSettings.GoogleAppId);
            //StartPushService();
            CrossPushNotification.Current.Register();
            CarouselViewRenderer.Init();
            CachedImageRenderer.Init(true);
        }
    }
}