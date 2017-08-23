using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;
using System.Linq;
using System.Collections.ObjectModel;

namespace referenceguide
{
    public class Person
    {
        public string FName { get; set; }
        public string LName { get; set; }

        public string FullName
        {
            get { return $"{FName} {LName}"; }
        }
    }
    public class SearchViewModel : ObservableViewModel, ISearchProvider
    {

        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
		public bool SearchIsDefaultAction { get; set; } = false;
		public string QueryHint { get; set; } = "Last Name";

        public ICommand SearchCommand { get; set; }

        public SearchViewModel()
        {
            People = GetPeople(null).ToObservable();

            SearchCommand = new RelayCommand((obj) =>
            {
                if (obj != null)
                    People = GetPeople((string)obj).ToObservable();
            });
        }


        private List<Person> GetPeople(string last)
        {
            var list = new List<Person>();
            list.Add(new Person() { FName = "Jack", LName = "Sparrow" });
            list.Add(new Person() { FName = "Charles", LName = "Brown" });
            list.Add(new Person() { FName = "Mack", LName = "Smith" });
            list.Add(new Person() { FName = "Henry", LName = "Abrams" });
            list.Add(new Person() { FName = "Milo", LName = "Davis" });
            list.Add(new Person() { FName = "Will", LName = "Piles" });
            list.Add(new Person() { FName = "Quincy", LName = "Yales" });
            list.Add(new Person() { FName = "Sally", LName = "Yules" });
            list.Add(new Person() { FName = "Thad", LName = "Dallon" });
            list.Add(new Person() { FName = "Randy", LName = "Tarvis" });
            list.Add(new Person() { FName = "Barry", LName = "Red" });
            list.Add(new Person() { FName = "Valerie", LName = "Spencer" });
            list.Add(new Person() { FName = "Mike", LName = "Spiegal" });
            list.Add(new Person() { FName = "Bob", LName = "Adams" });
            list.Add(new Person() { FName = "Ralph", LName = "Braun" });
            list.Add(new Person() { FName = "Vicky", LName = "Punders" });
            list.Add(new Person() { FName = "Micky", LName = "York" });
            list.Add(new Person() { FName = "Sam", LName = "Williams" });
            list.Add(new Person() { FName = "Hal", LName = "Weeks" });
            list.Add(new Person() { FName = "Paul", LName = "Gillis" });
            list.Add(new Person() { FName = "Nathan", LName = "Spears" });
            list.Add(new Person() { FName = "Eagar", LName = "Poe" });
            list.Add(new Person() { FName = "Greg", LName = "Gale" });
            list.Add(new Person() { FName = "Jules", LName = "Verner" });


            if (!string.IsNullOrEmpty(last))
            {
                list = list.Where(x => x.LName.StartsWith(last, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
            return list;
        }

    }
}
