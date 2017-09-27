using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class FontsMainPage : BoundPage<DataExampleViewModel>
    {
        public FontsMainPage()
        {
            this.Title = "Fonts";

            var fontAwesome = new CoreButton()
            {
                Text = "FontAwesome",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.FontAwesome;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var entypoPlus = new CoreButton()
            {
                Text = "EntypoPlus",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.EntypoPlus;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var ionicons = new CoreButton()
            {
                Text = "Ionicons",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.Ionicons;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var material = new CoreButton()
            {
                Text = "Material",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.Material;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var meteocons = new CoreButton()
            {
                Text = "Meteocons",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.Meteocons;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var simpleLineIcons = new CoreButton()
            {
                Text = "SimpleLineIcons",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.SimpleLineIcons;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var typicons = new CoreButton()
            {
                Text = "Typicons",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.Typicons;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            var weatherIcons = new CoreButton()
            {
                Text = "WeatherIcons",
                Style = AppStyles.LightOrange,
                Command = new Command(async (obj) =>
                {
                    VM.FontType = FontType.WeatherIcons;
                    VM.BuildResourceList();
                    await AppSettings.AppNav.PushAsync(new FontsCollectionView());
                })
            };

            Content = new StackLayout()
            {
                Padding = 20,
                Spacing = 10,
                Children = { fontAwesome, entypoPlus, ionicons, material, meteocons, simpleLineIcons, typicons, weatherIcons }
            };
        }
    }
}
