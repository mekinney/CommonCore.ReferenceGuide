using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AnimationsMain : BoundPage<AnimationsViewModel>
    {
        public AnimationsMain()
        {
            Title = "Animations";

            var bounceButton = CreateAnimation(
                "Bounce In",
                Color.Red,
				new BeginAnimation()
				{
					Animation = new BounceInAnimation()
					{
						Duration = "500"
					}
				}
            );

			var flipButton = CreateAnimation(
				"Flip",
				Color.Olive,
				new BeginAnimation()
				{
					Animation = new FlipAnimation()
					{
						Duration = "500"
					}
				}
			);

			var roateButton = CreateAnimation(
            	"Rotate",
            	Color.Gray,
            	new BeginAnimation()
            	{
            		Animation = new RotateToAnimation()
            		{
            			Duration = "500",
                        Rotation= 360
            		}
            	}
            );


			var fadeButton = CreateAnimation(
            	"Fade",
            	Color.ForestGreen,
            	new BeginAnimation()
            	{
            		Animation = new FadeToAnimation()
            		{
            			Duration = "500",
            			Opacity=0
            		}
            	}
            );

			var scaleButton = CreateAnimation(
            	"Scale",
            	Color.DarkTurquoise,
            	new BeginAnimation()
            	{
            		Animation = new ScaleToAnimation()
            		{
            			Duration = "500",
            			Scale=0.5
            		}
            	}
            );

            var translateButton = CreateAnimation(
                "Translate",
                Color.DarkOrange,
                new BeginAnimation()
                {
                    Animation = new TranslateToAnimation()
                    {
                        Duration = "250",
                        TranslateX = -5,
                        TranslateY = -32
                    }
                }
            );

			var turnsTileButton = CreateAnimation(
            	"Turnstile Out",
            	Color.Cyan,
            	new BeginAnimation()
            	{
            		Animation = new TurnstileOutAnimation()
            		{
            			Duration = "150",
            		}
            	}
            );

            var shakeButton = CreateAnimation(
                "Shake",
                Color.BlanchedAlmond,
                new BeginAnimation()
                {
                    Animation = new ShakeAnimation()
            	}
            );

			var heartBeatButton = CreateAnimation(
            	"Heartbeat",
            	Color.DarkRed,
            	new BeginAnimation()
            	{
            		Animation = new HeartAnimation()
            		{
            			Duration = "250"
            		}
            	}
            );

			var jumpButton = CreateAnimation(
            	"Jump",
            	Color.Aquamarine,
            	new BeginAnimation()
            	{
            		Animation = new HeartAnimation()
            		{
            			Duration = "500"
            		}
            	}
            );

			var storyboardButton = CreateAnimation(
            	"StoryBoard",
            	Color.Brown,
            	new BeginAnimation()
            	{
            		Animation = new StoryBoard()
            		{
                        Animations={
                            new ShakeAnimation(),
    						new ScaleToAnimation()
        					{
        						Duration = "500",
        						Scale=0.8
        					}
                        }
            		}
            	}
            );

            var dataTrigger = CreateDataTrigger();


			var container = new StackLayout()
			{
				Margin = 10,
				Spacing = 10,
				Children = {
					bounceButton,
					flipButton,
					roateButton,
					fadeButton,
					scaleButton,
					translateButton,
					turnsTileButton,
					shakeButton,
					heartBeatButton,
					jumpButton,
					storyboardButton,
                    dataTrigger
				}
			};

            Content = new ScrollView() { Content = container };
        }


        public Grid CreateAnimation(string buttonText, Color boxColor, BeginAnimation animation)
        {
            var box = new BoxView()
            {
                HeightRequest = 28,
                WidthRequest = 28,
                BackgroundColor = boxColor
            };

            animation.Animation.Target = box;

            var trigger = new EventTrigger()
            {
                Event = "Clicked",
                Actions = { animation }
            };

            var btn = new GradientButton()
            {
                Text = buttonText,
                Style = AppStyles.LightOrange,
                Triggers = { trigger }
            };

            var grid = new Grid();

            grid.AddChild(box, 0, 0);
            grid.AddChild(btn, 0, 1);
            return grid;
        }

		public Grid CreateDataTrigger()
		{
			var box = new BoxView()
			{
				HeightRequest = 28,
				WidthRequest = 28,
				BackgroundColor = Color.Crimson
			};

            var fadeAnimation = new BeginAnimation()
            {
                Animation = new FadeToAnimation()
                {
                    Target = box,
                    Duration = "300",
                    Opacity = 0
                }
            };

            var trigger = new DataTrigger(typeof(Button))
            {
                Binding = new Binding(path: "ClickCount", mode: BindingMode.TwoWay),
                Value = 3,
                EnterActions = { fadeAnimation }
            };

        
			var btn = new GradientButton()
			{
                Text = "Data Trigger (3)",
				Style = AppStyles.LightOrange,
				Triggers = { trigger }
			};
            btn.SetBinding(GradientButton.CommandProperty,"ClickEvent");

			var grid = new Grid();

			grid.AddChild(box, 0, 0);
			grid.AddChild(btn, 0, 1);
			return grid;
		}

    }
}
