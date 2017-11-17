using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CommunicationPage : CoreTelephonyPage<SimpleViewModel>
    {
		//defining class variables allows the the execution of the extension method SetAutomationIds
		private Entry phoneNum;
        private Entry message;
        private Entry emailAddr;
        private CoreButton btnSMS;
        private CoreButton btnEmail;
        private CoreButton btnCall;
        private CoreButton btnCallEvent;

        public CommunicationPage()
        {
            this.Title = "Communication";

            var lbl = new Label()
            {
                TextColor = Color.Gray,
                Text = "Phone Number",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            phoneNum = new CoreMaskedEntry()
            {
                MaskPattern="(###) ###-####",
                Margin = new Thickness(5, 1, 5, 1),
                Keyboard = Keyboard.Telephone,
                Placeholder = "(000) 000-0000",
            };
            phoneNum.SetBinding(Entry.TextProperty, "CommunicationNumber");

            var lblMsg = new Label()
            {
                TextColor = Color.Gray,
                Text = "Message",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            message = new Entry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                Placeholder = "Text",
            };
            message.SetBinding(Entry.TextProperty, "CommunicationMessage");


            var lblEmail = new Label()
            {
                TextColor = Color.Gray,
                Text = "Email Address",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            emailAddr = new Entry()
            {
                Margin = new Thickness(5, 1, 5, 10),
                Keyboard = Keyboard.Email,
                Placeholder = "somebody@provider.com",
            };
            emailAddr.SetBinding(Entry.TextProperty, "CommunicationEmail");

            btnSMS = new CoreButton()
            {
                Text = "Send SMS",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnSMS.SetBinding(CoreButton.CommandProperty, "SendSMS");

            btnEmail = new CoreButton()
            {
                Text = "Send Email",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnEmail.SetBinding(CoreButton.CommandProperty, "SendEmail");

            btnCall = new CoreButton()
            {
                Text = "Call",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnCall.SetBinding(CoreButton.CommandProperty, "MakeCall");

            btnCallEvent = new CoreButton()
            {
                Text = "Call With Event",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnCallEvent.SetBinding(CoreButton.CommandProperty, "MakeCallEvent");

            this.SetAutomationIds();
            Content = new CompressedStackLayout()
            {
                Padding = 15,
                Children = { lbl, phoneNum, lblEmail, emailAddr, lblMsg, message, btnSMS, btnEmail, btnCall, btnCallEvent }
            };
        }
    }
}
