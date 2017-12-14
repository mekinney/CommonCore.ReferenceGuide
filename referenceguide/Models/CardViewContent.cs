using System;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CardViewContent : CoreModel
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }

        public string FontFamily { get; set; }
        public string CountText { get; set; }

    }
}
