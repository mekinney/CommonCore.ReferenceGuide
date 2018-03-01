using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class DialogExample : ContentPage
    {
        public IDialogPrompt Prompt
        {
            get{
                return CoreDependencyService.GetDependency<DialogPrompt>();
            }
        }
        public DialogExample()
        {
            this.Title = "Dialogs";

            var dlg1 = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Ok Popup",
                Command = new Command(() => {
                    Prompt.ShowMessage(new Prompt()
                    {
                        Title = "Hello",
                        ButtonTitles = new string[]{"Yes"},
                        Message = "This is an okay message"
                    });
                })
            };

            var dlg2 = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Yes No Popup",
                Command = new Command(() => {
                    Prompt.ShowMessage(new Prompt()
                    {
                        Title = "Are you bored?",
                        Message = "Make a selection and change you life for the better",
                        ButtonTitles = new string[] { "Yes", "No" },
                        Callback = (obj) => {
                            var x = obj;
                        },
                    });
                })
            };

            var dlg3 = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Options Popup",
                Command = new Command(() =>
                {
                    Prompt.ShowMessage(new Prompt()
                    {
                        Title = "Which do you prefer?",
                        Message = "Pick a flavor and see what happens.",
                        ButtonTitles = new string[] { "Orange", "Apple", "Melon" },
                        Callback = (obj) =>
                        {
                            var x = obj;
                        },
                    });
                })
            };

            var dlg4 = new CoreButton()
            {
                Style = CoreStyles.LightOrange,
                Text = "Action Sheet",
                Command = new Command(() =>
                {
                    Prompt.ShowActionSheet(
                        "Vehicles",
                        "Please select your favorite",
                        new string[] {"Chevy","Ford","Lincoln","Nissan","Toyota","Honda","Audi"},
                        (obj) => {
                            var x = obj;
                    });
                })
            };

            var stack = new CompressedStackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { dlg1, dlg2, dlg3,dlg4 }
            };

            Content = new ScrollView()
            {
                Content = stack
            };
        }
    }
}
