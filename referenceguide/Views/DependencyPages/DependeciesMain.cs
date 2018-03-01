using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class DependeciesMain : CorePage<SimpleViewModel>
    {
        public DependeciesMain()
        {
            this.Title = "Dependencies";

            var dlg = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Dialog",
                AutomationId = "dialogButton",
                Command = new Command(async() => {
                    await Navigation.PushAsync(new DialogExample());
                })
            };
            //dlg.SetBinding(Button.CommandProperty, "DialogClick");


            var not = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Notification",
                AutomationId = "notifyButton"
            };
            not.SetBinding(Button.CommandProperty, "NotificationClick");


            var overlay = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Overlay",
                AutomationId = "overlayButton"
            };
            overlay.SetBinding(Button.CommandProperty, "OverlayClick");


            var blur = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Create Blur",
                AutomationId = "blurButton"
            };
            blur.SetBinding(Button.CommandProperty, "Blur");

            var snack = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "SnackBar",
                AutomationId = "snack"
            };
            snack.SetBinding(Button.CommandProperty, "ShowSnack");

            var playSound = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Play Sound",
                AutomationId = "playSound"
            };
            playSound.SetBinding(Button.CommandProperty, "PlaySound");


            var commTest = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Communication Dependencies",
                AutomationId = "commTest"
            };
            commTest.SetBinding(Button.CommandProperty, "CommTest");

            var cal = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Create Calendar Event",
                AutomationId = "calendarbutton"
            };
            cal.SetBinding(Button.CommandProperty, "CreateCalendar");

			var ctxMnu = new CoreButton()
			{
				Style = CoreStyles.LightOrange,
				Text = "Native Context Menu",
				AutomationId = "contextMenu"
			};
			ctxMnu.SetBinding(Button.CommandProperty, "ContextMenu");

            var pnRegister = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                AutomationId = "pnRegister"
            };
            pnRegister.SetBinding(Button.TextProperty, "PushButtonLabel");
            pnRegister.SetBinding(Button.CommandProperty, "PushRegister");

            var btnNav = new CoreButton()
            {
                Text = "Navigation Example",
                Style = CoreStyles.LightOrange,
                AutomationId = "btnNav",
                Command = new Command((obj) =>
                {
                    Navigation.PushNonAwaited<Nav1>();
                })
            };


            var stack = new CompressedStackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { dlg, not, overlay, blur, snack, playSound, commTest, ctxMnu, cal, pnRegister, btnNav, new StackLayout() { HeightRequest = 5 } }
            };

            Content = new ScrollView()
            {
                Content = stack
            };
        }

        protected override void OnAppearing()
        {
            this.SetAnalyticsTimeStamp();
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {

            this.SaveAnalyticsDetails();
            base.OnDisappearing();
        }
    }
}
