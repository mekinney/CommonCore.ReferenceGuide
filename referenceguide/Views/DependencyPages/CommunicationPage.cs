using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CommunicationPage : TelephonyBoundPage<CommunicationViewModel>
    {
		//defining class variables allows the the execution of the extension method SetAutomationIds
		private Entry phoneNum;
        private Entry message;
        private Entry emailAddr;
        private GradientButton btnSMS;
        private GradientButton btnEmail;
        private GradientButton btnCall;
        private GradientButton btnCallEvent;

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

            phoneNum = new MaskedTextField()
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

            btnSMS = new GradientButton()
            {
                Text = "Send SMS",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnSMS.SetBinding(GradientButton.CommandProperty, "SendSMS");

            btnEmail = new GradientButton()
            {
                Text = "Send Email",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnEmail.SetBinding(GradientButton.CommandProperty, "SendEmail");

            btnCall = new GradientButton()
            {
                Text = "Call",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnCall.SetBinding(GradientButton.CommandProperty, "MakeCall");

            btnCallEvent = new GradientButton()
            {
                Text = "Call With Event",
                Style = AppStyles.LightOrange,
                Margin = 5,
            };
            btnCallEvent.SetBinding(GradientButton.CommandProperty, "MakeCallEvent");

            this.SetAutomationIds();
            Content = new StackLayout()
            {
                Padding = 15,
                Children = { lbl, phoneNum, lblEmail, emailAddr, lblMsg, message, btnSMS, btnEmail, btnCall, btnCallEvent }
            };
        }
    }
}
