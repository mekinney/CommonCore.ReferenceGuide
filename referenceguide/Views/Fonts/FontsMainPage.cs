using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class FontsMainPage : BoundPage<FontsCollectionViewModel>
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
                    VM.FontName = FontFamilyEnum.FontAwesome;
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
                    VM.FontName = FontFamilyEnum.EntypoPlus;
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
                    VM.FontName = FontFamilyEnum.Ionicons;
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
                    VM.FontName = FontFamilyEnum.Material;
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
                    VM.FontName = FontFamilyEnum.Meteocons;
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
                    VM.FontName = FontFamilyEnum.SimpleLineIcons;
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
                    VM.FontName = FontFamilyEnum.Typicons;
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
                    VM.FontName = FontFamilyEnum.WeatherIcons;
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
