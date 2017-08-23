using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class DataExampleViewModel : ObservableViewModel
    {
        private WebDownloadClient downloadClient;

        public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();
        public ObservableCollection<RandomUser> RandomUsers { get; set; } = new ObservableCollection<RandomUser>();
        public string HashMatchMessage{ get; set; }
        public string ClearHash1{ get; set; }
        public string ClearHash2{ get; set; }
        public string ClearText{ get; set; }
        public string EncryptedText{ get; set; }
        public string BackgroundButtonTitle{ get; set; }


        public ICommand EncryptText { get; set; }
        public ICommand HashText { get; set; }
        public ICommand HttpDownloadStart { get; set; }
        public ICommand SqliteLoadStart { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        public ICommand StartBackgrounding { get; set; }
        public ICommand HttpPost { get; set; }
        public ICommand LongDownload { get; set; }

        public DataExampleViewModel()
        {
            BackgroundButtonTitle = "Background Timer";

            HashText = new RelayCommand((obj) =>
            {
                if (!string.IsNullOrEmpty(ClearHash1) && !string.IsNullOrEmpty(ClearHash2))
                {
                    var h1 = this.EncryptionService.GetHashString(ClearHash1);
                    var h2 = this.EncryptionService.GetHashString(ClearHash2);
                    var isMatch = h1.Equals(h2);
                    if (isMatch)
                        HashMatchMessage = string.Empty;
                    else
                        HashMatchMessage = "The entries do not match";
                }

            });
            EncryptText = new RelayCommand((obj) =>
            {
                if (string.IsNullOrEmpty(EncryptedText))
                {
                    if (!string.IsNullOrEmpty(ClearText))
                    {
                        EncryptedText = this.EncryptionService.AesEncrypt(ClearText, this.AESEncryptionKey);
                        ClearText = string.Empty;
                    }
                }
                else
                {
                    ClearText = this.EncryptionService.AesDecrypt(EncryptedText, this.AESEncryptionKey);
                    EncryptedText = string.Empty;
                }
            });

            HttpDownloadStart = new RelayCommand(async (obj) =>
            {
                await GetRandomUsers();
            });

            SqliteLoadStart = new RelayCommand(async (obj) =>
            {
                await GetDbAppointments();
            });

            StartBackgrounding = new RelayCommand((obj) =>
            {
                if (BackgroundButtonTitle.StartsWith("Stop", System.StringComparison.OrdinalIgnoreCase))
                {
                    this.BackgroundTimer.Stop();
                    BackgroundButtonTitle = "Background Timer";
                }
                else
                {
                    var timerService = InjectionManager.GetService<IIntervalCallback, TimerCallbackService>(true);
                    this.BackgroundTimer.Start(1, timerService);
                    BackgroundButtonTitle = $"Stop {BackgroundButtonTitle}";
                }
            });

            HttpPost = new RelayCommand(async (obj) =>
            {

                var url = this.WebApis["referencewebtestpost"];

                var p = new PostItem() { FirstName = "Jack", LastName = "Sparrow", Age = 21 };
                var response = await HttpService.Post<PostItem>(url, p);
                if (response.Success)
                {
                    var pp = response.Response;
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Success",
                        Message = $"The person's new id for {pp.FirstName} {pp.LastName}  is {pp.Id}"
                    });
                }
                else
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = response.Error.Message
                    });
                }

            });
            LongDownload = new RelayCommand(async(obj) => {
                downloadClient = HttpService.GetWebDownloadClient();
                downloadClient.DownloadUrl = this.WebApis["largefile"];
                downloadClient.PercentageChanged += (percent) => {
                    Device.BeginInvokeOnMainThread(()=>{
                        ProgressIndicator.ShowProgress("Downloading...", percent);
                    });
                };
                downloadClient.FinishedEvent += (data) => {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ProgressIndicator.Dismiss();
                    });
                    var fileData = data;
                };
                await downloadClient.StartDownload();

            });

        }

        public async Task GetDbAppointments()
        {
            this.LoadingMessageHUD = "Sqlite loading...";
            this.IsLoadingHUD = true;

            var result = await this.SqliteDb.GetByQuery<Appointment>((x) => x.MarkedForDelete == false);
            Log.LogResponse(result);

            this.IsLoadingHUD = false;
            if (result.Success)
            {
                Appointments = result.Response.ToObservable();
            }

        }

        private async Task GetRandomUsers()
        {
            this.LoadingMessageHUD = "Performing download...";
            this.IsLoadingHUD = true;

            var url = this.WebApis["randomuser"];

            //var isAvailable = await this.HttpService.PingDomain(url);

            var result = await this.HttpService.Get<RootObject>(url);
			Log.LogResponse(result);

            this.IsLoadingHUD = false;
            if (result.Success)
            {
                RandomUsers = result.Response.results.ToRandomUserObservableCollection();
            }

        }

        public override void OnViewMessageReceived(string key, object obj)
        {
            if (key == AppSettings.RefreshAppoints)
            {
                GetDbAppointments().ContinueWith((t) => { });
            }
        }

		public override void SaveState()
		{
			this.SaveState<DataExampleViewModel>();

		}
		public override void LoadState()
		{
			this.LoadState<DataExampleViewModel>();
		}
    }
}
