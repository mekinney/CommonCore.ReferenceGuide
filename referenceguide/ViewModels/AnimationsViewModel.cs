using System;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AnimationsViewModel:ObservableViewModel
    {
        public int ClickCount { get; set; }
        public ICommand ClickEvent { get; set; }

        public AnimationsViewModel()
        {
            ClickEvent = new RelayCommand((obj) => {
                ClickCount++;
            });
        }
    }
}
