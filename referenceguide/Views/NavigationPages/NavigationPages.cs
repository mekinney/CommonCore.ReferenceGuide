using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class Nav1 : BasePages
    {
        public Nav1()
        {
            this.Title = "Nav1";
            var btn = new GradientButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Navigate",
                AutomationId = "btn",
                Command = new Command(async (obj) =>
                {
                    
                    await AppSettings.AppNav.PushAsync(new Nav2());
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btn }
            };
        }
    }
    public class Nav2 : BasePages
    {
        public Nav2()
        {
            this.Title = "Nav2";
            var btn = new GradientButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Navigate",
                AutomationId = "btn",
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PushAsync(new Nav3());
                })
            };

            var btnBack = new GradientButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Back",
                AutomationId = "btnBack",
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PopAsync(true);
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btn, btnBack }
            };
        }
    }
    public class Nav3 : BasePages
    {
        public Nav3()
        {
            this.Title = "Nav3";
            var btn = new GradientButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Navigate",
                AutomationId = "btn",
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PushAsync(new Nav4());
                })
            };

            var btnBack = new GradientButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Back",
                AutomationId = "btnBack",
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PopTo<Nav1>(true);
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btn, btnBack }
            };
        }
    }
    public class Nav4 : BasePages
    {
        public Nav4()
        {
            this.Title = "Nav4";

            var btnBack = new GradientButton()
            {
                Style = AppStyles.LightOrange,
                Text = "Back",
                AutomationId = "btnBack",
                Command = new Command(async (obj) =>
                {
                    await AppSettings.AppNav.PopTo<Nav2>(true);
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { btnBack }
            };
        }
    }
}
