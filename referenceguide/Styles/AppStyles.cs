using System;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore.Styles;

namespace referenceguide
{
    public class AppStyles : CoreStyles
    {
        private static Style lightOrange;
        private static Style addressCell;
        private static Style fontLabel;

        public static Style LightOrange
        {
            get
            {
                return lightOrange ?? (
                    lightOrange = new Style(typeof(GradientButton))
                    {
                        Setters ={
                            new Setter(){Property=GradientButton.StartColorProperty ,Value=Color.FromHex("#DF8049")},
                            new Setter(){Property=GradientButton.EndColorProperty ,Value=Color.FromHex("#E8A47D")},
                            new Setter(){Property=GradientButton.ShadowColorProperty ,Value=Color.Gray},
                            new Setter(){Property=GradientButton.TextColorProperty ,Value=Color.White},
                            new Setter(){Property=GradientButton.ShadowOffsetProperty ,Value=1},
                            new Setter(){Property=GradientButton.ShadowOpacityProperty ,Value=1},
                            new Setter(){Property=GradientButton.ShadowRadiusProperty ,Value=Device.OnPlatform(6f,10f,6f)},
                            new Setter(){Property=GradientButton.CornerRadiusProperty ,Value=Device.OnPlatform(6f,10f,6f)},
                        }
                    });
            }
        }

        public static Style AddressCell
        {
            get
            {
                return addressCell ?? (
                    addressCell = new Style(typeof(Label))
                    {
                        Setters ={
                            new Setter(){Property=Label.TextProperty ,Value=Color.Gray},
                            new Setter(){Property=Label.FontSizeProperty ,Value=12},
                            new Setter(){Property=Label.MarginProperty ,Value=new Thickness(5,0,2,0)}
                        }
                    });
            }

        }

		public static Style FontLabel
		{
			get
			{
                return fontLabel ?? (
                    fontLabel = new Style(typeof(Label))
                    {
                        Setters ={
                            new Setter(){Property=Label.TextProperty ,Value=Color.Gray},
                            new Setter(){Property=Label.FontSizeProperty ,Value=24},
                            new Setter(){Property=Label.MarginProperty ,Value=10},
                            new Setter(){Property=Label.HorizontalOptionsProperty ,Value=LayoutOptions.Center},
                            new Setter(){Property=Label.HorizontalTextAlignmentProperty ,Value=LayoutOptions.Center},
                            new Setter(){Property=Label.FontFamilyProperty ,Value=OS=="IOS"?"Boxise":"BoxiseFont.otf#Boxise"}
                        }
                    });
			}

		}

        public static string OS
        {
            get { return Device.RuntimePlatform.ToUpper(); }
        }
    }
}
