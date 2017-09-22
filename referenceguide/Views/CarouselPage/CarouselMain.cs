using System;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CarouselMain:BoundPage<SimpleViewModel>
    {
        public CarouselMain()
        {
            this.Title = "Photos";
            var carousel = new CarouselViewControl()
            {
                HeightRequest=300,
                ItemTemplate = new PictureTemplateSelector(),
                ShowIndicators=true,
                IsSwipingEnabled=true,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.StartAndExpand,
                AnimateTransition = true,
                IndicatorsShape = IndicatorsShape.Square,
                InterPageSpacing = 10,
                Orientation= CarouselViewOrientation.Horizontal,
            };
            carousel.SetBinding(CarouselViewControl.ItemsSourceProperty,"ItemSource");
            carousel.SetBinding(CarouselViewControl.PositionProperty,"Position");

            var lbl = new Label()
            {
                Margin = 10,
                Text = "Bacon ipsum dolor amet jerky picanha beef chicken ball tip, capicola shoulder pork belly boudin prosciutto shank sausage pig hamburger. Tongue pork cupim landjaeger chuck short loin kielbasa fatback tail strip steak. Spare ribs kielbasa tenderloin jerky alcatra tri-tip pork. Spare ribs jowl shankle, ball tip alcatra ham short ribs picanha chicken drumstick. Cupim corned beef bacon, shoulder brisket ground round leberkas bresaola.\n\nT-bone beef ribs pastrami chuck. T-bone tongue swine bacon picanha, tenderloin beef strip steak. Pork loin sirloin picanha, short loin bresaola brisket alcatra corned beef venison sausage prosciutto cupim turkey boudin chicken. Sirloin capicola cupim chuck alcatra pork. Pork jerky fatback, meatball short loin tri-tip doner beef ribs bacon porchetta."
            };

            Content = new StackLayout()
            {
                Children = { carousel, new ScrollView() { Content = lbl } }
            };
        }
    }
}
