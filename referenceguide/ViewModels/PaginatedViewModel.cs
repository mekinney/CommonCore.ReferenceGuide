using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class PaginatedViewModel : ObservableViewModel
    {
        private RandomUser selectedUser;
        private ObservableCollection<RandomUser> randomUsers;
        private int pageIndex = 0;
		public ObservableCollection<RandomUser> RandomUsers
		{
			get { return randomUsers ?? (randomUsers = new ObservableCollection<RandomUser>()); }
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
            LoadMore = new RelayCommand(async(obj) => {
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
			this.IsLoadingHUD = false;
			if (result.Success)
			{
                result.Response.results.ToRandomUserList().ForEach((item) => { RandomUsers.Add(item); });
			}

		}
    }
}
