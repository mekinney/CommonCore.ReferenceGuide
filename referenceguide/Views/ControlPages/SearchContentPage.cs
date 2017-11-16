using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class PeopleCell : ViewCell
    {
        public PeopleCell()
        {
            var lbl = new Label() { Margin = 10 };
            lbl.SetBinding(Label.TextProperty, "FullName");
            View = new StackLayout() { Children = { lbl } };
        }

    }

    public class SearchContentPage : CorePage<SearchViewModel>
    {
        private SearchBar searchBar;
        public SearchContentPage()
        {
            this.Title = "Search Page";

            var lstPeople = new CoreListView()
            {
                ItemTemplate = new DataTemplate(typeof(PeopleCell))
            };
            lstPeople.SetBinding(CoreListView.ItemsSourceProperty, "People");

            StackLayout container = null;
            if(Device.RuntimePlatform=="iOS"){
                searchBar = new SearchBar() { 
                    SearchCommand=new Command((obj) => {
                        VM.SearchCommand.Execute(searchBar.Text);
                    })
                };
				Content = new StackLayout()
				{
					Children = { searchBar, lstPeople }
				};
            }
            else{
				Content = new StackLayout()
				{
					Children = { lstPeople }
				};
            }

        }
    }
}

