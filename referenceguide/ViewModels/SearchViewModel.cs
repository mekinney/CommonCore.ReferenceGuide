using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections.ObjectModel;

namespace referenceguide
{

    public class SearchViewModel : CoreViewModel, ISearchProvider
    {
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
        public bool SearchIsDefaultAction { get; set; } = false;
        public string QueryHint { get; set; } = "Last Name";
        public ICommand SearchCommand { get; set; }

        public SearchViewModel()
        {
            SearchCommand = new CoreCommand(SearchCommandMethod);
        }

        private void SearchCommandMethod(object obj)
        {
            if (obj != null)
            {
                var peopleResults = DataBLL.GetPeople((string)obj);
                if (peopleResults.Error == null)
                    People = peopleResults.Response.ToObservable();
            }
        }

        public override void LoadResources(string parameter = null)
        {
            var peopleResults = DataBLL.GetPeople(null);
            if (peopleResults.Error == null)
                People = peopleResults.Response.ToObservable();
        }

    }
}
