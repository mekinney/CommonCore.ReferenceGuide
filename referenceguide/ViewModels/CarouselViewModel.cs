using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CarouselBindingObject
    {
        public string ImageUrl { get; set; }
        public int Position { get; set; }
    }
    public class CarouselViewModel: ObservableViewModel
    {
        public int Position{ get; set; }
        public ObservableCollection<string> Images{ get; set; }

        public ObservableCollection<CarouselBindingObject> ItemSource{ get; set; }

        public CarouselViewModel()
        {
            ItemSource = new ObservableCollection<CarouselBindingObject>(new CarouselBindingObject[]{
                new CarouselBindingObject(){ Position=1, ImageUrl="snowfall.jpeg"},
                new CarouselBindingObject(){Position=2, ImageUrl="rain.jpeg"},
                new CarouselBindingObject(){Position=3, ImageUrl="beach.jpeg"},
                new CarouselBindingObject(){Position=4, ImageUrl="sunset.jpeg"}
            });
            Images = new ObservableCollection<string>() { "", "", "", "" };
        }

    }
}
