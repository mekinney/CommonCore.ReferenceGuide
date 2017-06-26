using System;
using Xamarin.Forms.CommonCore;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore.Styles;

namespace referenceguide
{
	public class AppStyles: CoreStyles
	{
		public static Style _lightOrange;
		public static Style _lightBlue;
		public static Style _addressCell;

		public static Style LightOrange
		{
			get
			{
				if (_lightOrange == null)
				{
					_lightOrange = new Style(typeof(GradientButton))
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
					};
				}
				return _lightOrange;
			}
			set
			{
				_lightOrange = value;
			}
		}

		public static Style AddressCell
		{
			get
			{
				if (_addressCell == null)
				{
					_addressCell = new Style(typeof(Label))
					{
						Setters ={
							new Setter(){Property=Label.TextProperty ,Value=Color.Gray},
							new Setter(){Property=Label.FontSizeProperty ,Value=12},
							new Setter(){Property=Label.MarginProperty ,Value=new Thickness(5,0,2,0)}
						}
					};
				}
				return _addressCell;
			}
			set
			{
				_addressCell = value;
			}
		}
	}
}
