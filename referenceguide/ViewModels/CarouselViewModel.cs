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
        private ObservableCollection<CarouselBindingObject> itemSource;
        private ObservableCollection<string> images;
        private int position;

        public int Position
        {
            get { return position; }
            set { SetProperty(ref position, value); }
        }
        public ObservableCollection<string> Images
        {
            get { return images ?? (images = new ObservableCollection<string>()); }
            set { SetProperty(ref images, value); }
        }

        public ObservableCollection<CarouselBindingObject> ItemSource
        {
            get { return itemSource ?? (itemSource = new ObservableCollection<CarouselBindingObject>()); }
            set { SetProperty(ref itemSource, value); }
        }

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
