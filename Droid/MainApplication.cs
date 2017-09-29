using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Plugin.CurrentActivity;
using PushNotification.Plugin; //Xam.Plugin.PushNotification from nuget or get from github and compile to latest version
using Xamarin.Forms.CommonCore;

namespace referenceguide.Droid
{

    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public static Context AppContext;

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

#if DEBUG
            AppSettings.CurrentBuid = "dev";
#elif QA
            AppSettings.CurrentBuid = "qa";
#elif RELEASE
			AppSettings.CurrentBuid = "prod";
#endif

            RegisterActivityLifecycleCallbacks(this);


            InitGlobalLibraries();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            InjectionManager.GetService<ILogService, LogService>().LogException(
                unobservedTaskExceptionEventArgs.Exception,
                "MainApplication -TaskSchedulerOnUnobservedTaskException");

            unobservedTaskExceptionEventArgs.Exception.ConsoleWrite();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var ex = unhandledExceptionEventArgs.ExceptionObject as Exception;
            InjectionManager.GetService<ILogService, LogService>().LogException(
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
            AppSettings.AppIcon = Resource.Drawable.icon;
            AppContext = this.ApplicationContext;
            LocalNotify.MainType = typeof(MainActivity);

			CachedImageRenderer.Init();
			CarouselViewRenderer.Init();

			CrossPushNotification.Initialize<CrossPushNotificationListener>(AppSettings.Config.SocialMedia.GoogleSettings.GoogleAppId);

			StartPushService();

			CrossPushNotification.Current.Register();
        }
    }
}