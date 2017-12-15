using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class EncryptionPage : CorePage<DataExampleViewModel>
    {
        public EncryptionPage()
        {
            this.Title = "Encryption";

            var lbl = new Label()
            {
                TextColor = Color.Gray,
                Text = "Clear Text",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var clearEntry = new CoreUnderlineEntry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "clearEntry"
            };
            clearEntry.SetBinding(CoreUnderlineEntry.TextProperty, "ClearText");

            var encryptedLabel = new Label()
            {
                FontSize = 14,
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "encryptedLabel"
            };
            encryptedLabel.SetBinding(Label.TextProperty, "EncryptedText");

            var btnEncrypt = new CoreButton()
            {
                Text = "Encryption",
                Style = CoreStyles.LightOrange,
                Margin = 5,
                AutomationId = "btnEncrypt"
            };
            btnEncrypt.SetBinding(Button.CommandProperty, "EncryptText");


            var md5Label1 = new Label()
            {
                TextColor = Color.Gray,
                Text = "Hash 1",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1),
                AutomationId = "md5Label1"
            };
            var clearHash1 = new CoreUnderlineEntry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "clearHash1"
            };
            clearHash1.SetBinding(CoreUnderlineEntry.TextProperty, "ClearHash1");

            var md5Label2 = new Label()
            {
                TextColor = Color.Gray,
                Text = "Hash 2",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1),
                AutomationId = "md5Label2"
            };
            var clearHash2 = new CoreUnderlineEntry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "clearHash2"
            };
            clearHash2.SetBinding(CoreUnderlineEntry.TextProperty, "ClearHash2");

            var btnHash = new CoreButton()
            {
                Text = "Compare Clear Hash",
                Style = CoreStyles.LightOrange,
                Margin = 5,
                AutomationId = "btnHash"
            };
            btnHash.SetBinding(Button.CommandProperty, "HashText");

            var hashMatch = new Label()
            {
                TextColor = Color.Red,
                AutomationId = "hashMatch"
            };
            hashMatch.SetBinding(Label.TextProperty, "HashMatchMessage");

            Content = new CompressedStackLayout()
            {
                Padding = 15,
                Children = { lbl, clearEntry, encryptedLabel, btnEncrypt, md5Label1, clearHash1, md5Label2, clearHash2, btnHash, hashMatch }
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
