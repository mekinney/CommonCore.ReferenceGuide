using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class BehaviorsMain : BoundPage<SimpleViewModel>
    {
        private RegExBehavior nameRequiredValidator;
        private RegExBehavior phoneRequiredValidator;
        private PhoneMaskBehavior phoneMask;
        private PropertyChangedBehavior propBehavior;

        public BehaviorsMain()
        {
            this.Title = "Behaviors";


            nameRequiredValidator = new RegExBehavior()
            {
                ErrorMessage = "Requred Field",
                RegexExp = @"^[\s\t\r\n]*\S+"
            };
            phoneRequiredValidator = new RegExBehavior()
            {
                ErrorMessage = "Requred Field",
                RegexExp = @"^[\s\t\r\n]*\S+"
            };
            phoneMask = new PhoneMaskBehavior();

            propBehavior = new PropertyChangedBehavior(VM, (prop, ctrl) =>
            {
                if (prop == "FirstName")
                {
                    if (VM.FirstName == "Jack Sparrow")
                    {
                        var ctrlLabel = (Label)ctrl;
                        ctrlLabel.IsVisible = true;
                        ctrlLabel.TextColor = Color.Green;
                        ctrlLabel.Text = "You like a pirate that has a bird for a friend!";
                    }
                    else
                    {
                        ctrl.IsVisible = false;
                    }
                }
            });

            var explanation = new Label()
            {
                AutomationId = "explanation",
                Margin = new Thickness(5, 5, 5, 15),
                Text = "Enter and remove content in the fields below to see behaviors work.  Enter the name 'Jack Sparrow' to see custom behavior."
            };

            var lbl = new Label()
            {
                TextColor = Color.Gray,
                Text = "First Name",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var fNameEntry = new Entry()
            {
                AutomationId = "fNameEntry",
                Margin = new Thickness(5, 1, 5, 1)
            };
            fNameEntry.SetBinding(Entry.TextProperty, "FirstName");
            fNameEntry.Behaviors.Add(nameRequiredValidator);

            var errorLabel = new Label()
            {
                FontSize = 14,
                TextColor = Color.Red,
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "errorLabel"
            };
            errorLabel.SetBinding(Label.TextProperty, new Binding(source: nameRequiredValidator, path: "ErrorMessage", mode: BindingMode.OneWay));
            errorLabel.SetBinding(Label.IsVisibleProperty, new Binding(source: nameRequiredValidator, path: "HasError", mode: BindingMode.OneWay));


            var lblPhone = new Label()
            {
                TextColor = Color.Gray,
                Text = "Phone Number",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1),
                AutomationId = "lblPhone"
            };
            var phoneEntry = new Entry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "phoneEntry"
            };
            phoneEntry.Behaviors.Add(phoneMask);
            phoneEntry.Behaviors.Add(phoneRequiredValidator);

            var phoneErrorLabel = new Label()
            {
                FontSize = 14,
                TextColor = Color.Red,
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "phoneErrorLabel"
            };
            phoneErrorLabel.SetBinding(Label.TextProperty, new Binding(source: phoneRequiredValidator, path: "ErrorMessage", mode: BindingMode.OneWay));
            phoneErrorLabel.SetBinding(Label.IsVisibleProperty, new Binding(source: phoneRequiredValidator, path: "HasError", mode: BindingMode.OneWay));


            var customLabel = new Label() { Margin = 5, AutomationId = "customLabel" };
            customLabel.Behaviors.Add(propBehavior);

            Content = new StackLayout()
            {
                Padding = 15,
                Children = { explanation, lbl, fNameEntry, errorLabel, lblPhone, phoneEntry, phoneErrorLabel, customLabel }
            };
        }
    }
}
