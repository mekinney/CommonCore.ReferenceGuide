using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class BackgroundImagePage: BoundPage<SimpleViewModel>
    {
        public BackgroundImagePage()
        {
            var imgName = Device.RuntimePlatform == "iOS" ? "Default.png" : "screen.png";
            this.BackgroundImage = imgName;

            var formattedString = new FormattedString();
            formattedString.AddTextSpan("\tSimply setting the background image in iOS may result in the image tiling across the screen. Some articles suggest using an absolute layout with a bottom layer being an image. In the readme.txt find a url to create icons and splash screen assets and place them in your project. \n\n");
            formattedString.AddTextSpan("\tSet the page's BackgroundImage property and the BasePageRenderer in the CommonCore will fix the tiling issue without you having to layer controls to accomplish the effect.");
            var lbl = new Label()
            {
                FormattedText =formattedString
            };

            var frame = new Frame()
            {
                Margin = 10,
                Padding = 10,
                BackgroundColor = Color.White,
                Opacity = 0.5,
                Content = new StackLayout()
                {
                    Children = { lbl }
                }
            };


            Content = new StackLayout()
            {
                Children = { frame }
            };
        }
    }
}
