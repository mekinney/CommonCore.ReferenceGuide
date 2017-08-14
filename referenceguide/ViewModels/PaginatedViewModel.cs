using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	/*
     * This view model example uses the OptimizedObservableCollection to perform mass loading without 
     * notifying the UI of each item (preventing individual redrawing)
     */
	public class PaginatedViewModel : ObservableViewModel
    {
        private RandomUser selectedUser;
        private OptimizedObservableCollection<RandomUser> randomUsers;
        private int pageIndex = 0;
        public OptimizedObservableCollection<RandomUser> RandomUsers
        {
            get { return randomUsers ?? (randomUsers = new OptimizedObservableCollection<RandomUser>()); }
            set { SetProperty(ref randomUsers, value); }
        }

        public ICommand LoadMore { get; set; }

        public RandomUser SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref selectedUser, value);
                    PageTitle = selectedUser?.FullName;
                }
            }
        }

        public PaginatedViewModel()
        {
            LoadMore = new RelayCommand(async (obj) =>
            {
                await GetRandomUsers();
            });

            Task.Run(async () => { await GetRandomUsers(); });
        }

        private async Task GetRandomUsers()
        {
            PageTitle = "Paginate";
            this.LoadingMessageHUD = "Performing download...";
            this.IsLoadingHUD = true;

            var url = this.WebApis["randomuserpaginated"];
            url = string.Format(url, pageIndex);
            pageIndex++;

            var result = await this.HttpService.Get<RootObject>(url);
            Log.LogResponse(result);

            this.IsLoadingHUD = false;
            if (result.Success)
            {
                using (var updated = RandomUsers.BeginMassUpdate())
                {
                    randomUsers.AddRange(result.Response.results.ToRandomUserList());
                }
            }

        }
    }
}
