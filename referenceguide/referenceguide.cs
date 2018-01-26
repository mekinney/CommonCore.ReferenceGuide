using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System;

namespace referenceguide
{
    public class App : Application
    {
        public App()
        {
            LocalizationService.Init("1.0");
            CoreSettings.NotificationTags.Add("referenceguide");
            MainPage = new MainPage();
        }

        private void AppScreenSizeChanged(object sender, EventArgs args)
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
        }

        protected override void OnStart()
        {
            CoreSettings.ScreenSize = new Size(MainPage.Width, MainPage.Height);
            MainPage.SizeChanged += AppScreenSizeChanged;
        }

        protected override void OnSleep()
        {
            MainPage.SizeChanged -= AppScreenSizeChanged;
        }

        protected override void OnResume()
        {
            MainPage.SizeChanged += AppScreenSizeChanged;
        }

    }
}
