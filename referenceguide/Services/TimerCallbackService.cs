using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class TimerCallbackService : IIntervalCallback
    {
        public void TimeElapsedEvent()
        {
            //example of routing execution to some view model in the application
            var vm = InjectionManager.GetViewModel<SimpleViewModel>();
            vm.DisplayNotification(new LocalNotification()
            {
                Id = 1,
                Title = "Timer Event",
                Icon = "icon.png",
                Message = $"The timer event fired {DateTime.Now.ToShortTimeString()}"
            });

#if DEBUG
            Console.WriteLine("*****************   I am a little teapot *****************************");
#endif

        }
    }
}
