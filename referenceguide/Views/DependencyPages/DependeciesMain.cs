using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class DependeciesMain : BoundPage<SimpleViewModel>
    {
        public DependeciesMain()
        {
            this.Title = "Dependencies";

            var dlg = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Dialog",
                AutomationId = "dialogButton"
            };
            dlg.SetBinding(Button.CommandProperty, "DialogClick");


            var not = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Notification",
                AutomationId = "notifyButton"
            };
            not.SetBinding(Button.CommandProperty, "NotificationClick");


            var overlay = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Overlay",
                AutomationId = "overlayButton"
            };
            overlay.SetBinding(Button.CommandProperty, "OverlayClick");


            var blur = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Create Blur",
                AutomationId = "blurButton"
            };
            blur.SetBinding(Button.CommandProperty, "Blur");

            var snack = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "SnackBar",
                AutomationId = "snack"
            };
            snack.SetBinding(Button.CommandProperty, "ShowSnack");

            var playSound = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Play Sound",
                AutomationId = "playSound"
            };
            playSound.SetBinding(Button.CommandProperty, "PlaySound");


            var commTest = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Communication Dependencies",
                AutomationId = "commTest"
            };
            commTest.SetBinding(Button.CommandProperty, "CommTest");

            var cal = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Create Calendar Event",
                AutomationId = "calendarbutton"
            };
            cal.SetBinding(Button.CommandProperty, "CreateCalendar");

			var ctxMnu = new CoreButton()
			{
				Style = AppStyles.LightOrange,
				Text = "Native Context Menu",
				AutomationId = "contextMenu"
			};
			ctxMnu.SetBinding(Button.CommandProperty, "ContextMenu");

            var pnRegister = new CoreButton()
            {
                Style = AppStyles.LightOrange,
                AutomationId = "pnRegister"
            };
            pnRegister.SetBinding(Button.TextProperty, "PushButtonLabel");
            pnRegister.SetBinding(Button.CommandProperty, "PushRegister");

            var btnNav = new CoreButton()
            {
                Text = "Navigation Example",
                Style = AppStyles.LightOrange,
                AutomationId = "btnNav",
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PushAsync(new Nav1());
                })
            };


            var stack = new StackLayout()
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
    }
}
