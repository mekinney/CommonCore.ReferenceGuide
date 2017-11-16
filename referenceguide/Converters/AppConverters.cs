using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AppConverters
    {
        public static UpperTextConverter UpperText
        {
            get
            {
                return CoreDependencyService.GetConverter<UpperTextConverter>();
            }
        }
    }
}
