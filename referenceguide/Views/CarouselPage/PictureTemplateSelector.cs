using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace referenceguide
{
    public class PictureTemplateSelector : DataTemplateSelector
    {
        private List<DataTemplate> lst;

		public PictureTemplateSelector()
		{
            lst = new List<DataTemplate>();
		}

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
            var obj = (CarouselBindingObject)item;
           
            if (lst.Count >= obj.Position)
            {
                return lst[(obj.Position-1)];
            }
            else
            {
                var temp = new DataTemplate(typeof(PictureView));
                lst.Add(temp);
                return temp;
            }
		}
    }
}



