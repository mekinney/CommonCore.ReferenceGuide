using System;

using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace CommonCore.XamlReferenceGuide.Views
{
    public class BehaviorsPage : BasePages
    {
        public BehaviorsPage()
        {
            NeedOverrideSoftBackButton = true;
            OverrideBackButton = true;
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }


    }
}

