using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class BehaviorsMain : BoundPage<SimpleViewModel>
    {
        
        public BehaviorsMain()
        {
            this.Title = "Behaviors";

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

            var fNameEntry = new CoreUnderlineEntry()
            {
                AutomationId = "fNameEntry",
                Margin = new Thickness(5, 1, 5, 1),
                EntryColor = Color.DarkGray
            };
            fNameEntry.SetBinding(Entry.TextProperty, "FirstName");
            fNameEntry.Behaviors.Add(new RegExBehavior()
            {
                ErrorMessage = "Requred Field",
                RegexExp = @"^[\s\t\r\n]*\S+"
            });

            var errorLabel = new Label()
            {
                FontSize = 14,
                TextColor = Color.Red,
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "errorLabel"
            };

            errorLabel.SetBinding(Label.TextProperty, new Binding(source: fNameEntry.Behaviors[0], path: "ErrorMessage", mode: BindingMode.OneWay));
            errorLabel.SetBinding(Label.IsVisibleProperty, new Binding(source: fNameEntry.Behaviors[0], path: "HasError", mode: BindingMode.OneWay));


            var lblPhone = new Label()
            {
                TextColor = Color.Gray,
                Text = "Phone Number",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1),
                AutomationId = "lblPhone"
            };
            var phoneEntry = new CoreUnderlineEntry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "phoneEntry",
                EntryColor = Color.DarkGray
            };
            phoneEntry.Behaviors.Add(new PhoneMaskBehavior());
            phoneEntry.Behaviors.Add(new RegExBehavior()
            {
                ErrorMessage = "Requred Field",
                RegexExp = @"^[\s\t\r\n]*\S+"
            });

            var phoneErrorLabel = new Label()
            {
                FontSize = 14,
                TextColor = Color.Red,
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "phoneErrorLabel"
            };
            phoneErrorLabel.SetBinding(Label.TextProperty, new Binding(source: phoneEntry.Behaviors[1], path: "ErrorMessage", mode: BindingMode.OneWay));
            phoneErrorLabel.SetBinding(Label.IsVisibleProperty, new Binding(source: phoneEntry.Behaviors[1], path: "HasError", mode: BindingMode.OneWay));


			var lblBindingEvent = new Label()
			{
				TextColor = Color.Gray,
                Text = "Event To Command Binding - (numbers only)",
				FontSize = 14,
				Margin = new Thickness(5, 5, 5, 1)
			};
			var bindingEntry = new CoreUnderlineEntry()
			{
				Margin = new Thickness(5, 1, 5, 1),
				AutomationId = "bindingEntry",
                EntryColor = Color.DarkGray
			};

            bindingEntry.Behaviors.Add(new EventToCommandBehavior()
            {
                EventName = "TextChanged"
            });
            bindingEntry.Behaviors[0].SetBinding(EventToCommandBehavior.CommandProperty, "BindingTextChanged");
            bindingEntry.SetBinding(CoreUnderlineEntry.TextProperty,"BindingTextValue");

            var customLabel = new Label() { Margin = 5, AutomationId = "customLabel" };
            customLabel.Behaviors.Add(new PropertyChangedBehavior(VM, (prop, ctrl) =>
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
            }));

            var btnCanExecute = new CoreButton()
            {
                Margin = new Thickness(5, 5, 5, 1) ,
                Text="Can Execute",
                Style = AppStyles.LightOrange
            };
            btnCanExecute.SetBinding(CoreButton.CommandProperty,"CanExecute");

            Content = new StackLayout()
            {
                Padding = 15,
                Children = { explanation, lbl, fNameEntry, errorLabel, lblPhone, phoneEntry, phoneErrorLabel, customLabel, lblBindingEvent, bindingEntry, btnCanExecute }
            };
        }
    }
}
