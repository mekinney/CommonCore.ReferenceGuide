using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class DataExampleViewModel : CoreViewModel
    {

        private RandomUser selectedPagingatedUser;
        private int pageIndex = 0;

        public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();
        public ObservableCollection<RandomUser> RandomUsers { get; set; } = new ObservableCollection<RandomUser>();
        public ObservableCollection<ErrorLog> ErrorLogs { get; set; } = new ObservableCollection<ErrorLog>();
        public ObservableCollection<AnalyticLog> AnalyticLogs { get; set; } = new ObservableCollection<AnalyticLog>();
        public ObservableCollection<FontItemRow> Items { get; set; } = new ObservableCollection<FontItemRow>();
        public OptimizedObservableCollection<RandomUser> PaginatedRandomUsers { get; set; } = new OptimizedObservableCollection<RandomUser>();

        public string HashMatchMessage { get; set; }
        public string ClearHash1 { get; set; }
        public string ClearHash2 { get; set; }
        public string ClearText { get; set; }
        public string EncryptedText { get; set; }
        public string BackgroundButtonTitle { get; set; }
        public FontType FontType { get; set; }
        public RandomUser SelectedPagingatedUser
        {
            get { return selectedPagingatedUser; }
            set
            {
                if (value != null && selectedPagingatedUser != value)
                {
                    selectedPagingatedUser = value;
                    OnPropertyChanged("SelectedPagingatedUser");
                    PageTitle = selectedPagingatedUser?.FullName;
                }
            }
        }

        public ICommand CreateErrorEntry { get; set; } 
        public ICommand ClearErrorEntries { get; set; }
        public ICommand ClearAnalyticEntries { get; set; }
        public ICommand EncryptText { get; set; }
        public ICommand HashText { get; set; }
        public ICommand HttpDownloadStart { get; set; }
        public ICommand SqliteLoadStart { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        public ICommand StartBackgrounding { get; set; }
        public ICommand HttpPost { get; set; }
        public ICommand LongDownload { get; set; }
        public ICommand LoadMorePaginatedUsers { get; set; }


        public DataExampleViewModel()
        {
            BackgroundButtonTitle = "Background Timer";

            CreateErrorEntry = new CoreCommand(CreateErrorEntryMethod);
            ClearErrorEntries = new CoreCommand(ClearErrorEntriesMethod);
            ClearAnalyticEntries = new CoreCommand(ClearAnalyticEntriesMethod);
            LoadMorePaginatedUsers = new CoreCommand(GetPaginatedRandomUsers);
            HashText = new CoreCommand(HashTextMethod);
            EncryptText = new CoreCommand(EncryptTextMethod);
            HttpDownloadStart = new CoreCommand(GetRandomUsers);
            SqliteLoadStart = new CoreCommand(GetDbAppointments);
            StartBackgrounding = new CoreCommand(StartBackgroundingMethod);
            HttpPost = new CoreCommand(HttpPostMethod);
            LongDownload = new CoreCommand(LongDownloadMethod);

        }

        private void LongDownloadMethod(object obj)
        {
            WebBll.GetLongDownload((percent) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressIndicator.ShowProgress("Downloading...", percent);
                });
            }, (error) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressIndicator.Dismiss();
                    this.DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = error.Message
                    });

                });


            }).ContinueWith((data) => { 
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressIndicator.Dismiss();
                    var file = data;
                });
            });
        }

        private void HttpPostMethod(object obj)
        {
            Task.Run(async()=>{
                var result = await WebBll.PostDataExample();
                if (result.Error == null)
                {
                    var pp = result.Response;
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
                        Message = result.Error.Message
                    });
                }

            });
        }

        private void StartBackgroundingMethod(object obj)
        {
            if (BackgroundButtonTitle.StartsWith("Stop", System.StringComparison.OrdinalIgnoreCase))
            {
                DataBLL.TimerService.Stop();
                BackgroundButtonTitle = "Background Timer";
            }
            else
            {
                var timerService = CoreDependencyService.GetService<IIntervalCallback, TimerCallbackService>(true);
                DataBLL.TimerService.Start(1, timerService);
                BackgroundButtonTitle = $"Stop {BackgroundButtonTitle}";
            }
        }

        private void EncryptTextMethod(object obj)
        {
            if (string.IsNullOrEmpty(EncryptedText))
            {
                if (!string.IsNullOrEmpty(ClearText))
                {
                    var result = CryptoBLL.EncryptText(ClearText);
                    if (result.Error == null)
                    {
                        EncryptedText = result.Response;
                        ClearText = string.Empty;
                    }
                    else
                    {
                        DialogPrompt.ShowMessage(new Prompt()
                        {
                            Title = "Error",
                            Message = result.Error.Message
                        });
                    }

                }
            }
            else
            {

                var result = CryptoBLL.DecryptText(EncryptedText);
                if (result.Error == null)
                {
                    ClearText = result.Response;
                    EncryptedText = string.Empty;
                }
                else
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = result.Error.Message
                    });
                }
            }
        }

        private void HashTextMethod(object obj)
        {
            if (!string.IsNullOrEmpty(ClearHash1) && !string.IsNullOrEmpty(ClearHash2))
            {
                var isMatch = CryptoBLL.HashValueMatch(ClearHash1, ClearHash2);
                if (isMatch.Success)
                    HashMatchMessage = string.Empty;
                else
                    HashMatchMessage = "The entries do not match";
            }
        }

        private void ClearAnalyticEntriesMethod(object obj)
        {
            Task.Run(async () => {
                var result = await DataBLL.ClearLogFiles(LogType.Analytic);
                if (result.Success)
                    AnalyticLogs = new ObservableCollection<AnalyticLog>();

            });

        }

        private void ClearErrorEntriesMethod(object obj)
        {
            Task.Run(async () => {
                var result = await DataBLL.ClearLogFiles(LogType.Error);
                if (result.Success)
                    ErrorLogs = new ObservableCollection<ErrorLog>();
            });

        }

        private void CreateErrorEntryMethod(object obj)
        {
            Task.Run(async () => {
                DataBLL.CreateFictiousError();
                var result = await DataBLL.GetLogFiles<ErrorLog>();
                if (result.Error == null)
                    ErrorLogs = result.Response.ToObservable<ErrorLog>();
            });

        }


        public void GetPaginatedRandomUsers(object obj)
        {
            Task.Run(async () =>
            {
                this.LoadingMessageHUD = "Performing download...";
                this.IsLoadingHUD = true;

                var result = await WebBll.GetPaginatedRandomUsers(pageIndex);
                pageIndex++;

                this.IsLoadingHUD = false;
                if (result.Error == null)
                {
                    using (var updated = PaginatedRandomUsers.BeginMassUpdate())
                    {
                        PaginatedRandomUsers.AddRange(result.Response);
                    }
                }
                else
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = result.Error.Message
                    });
                }

            });

        }

        public void GetDbAppointments(object obj)
        {
            Task.Run(async () => {
                this.LoadingMessageHUD = "Sqlite loading...";
                this.IsLoadingHUD = true;

                var result = await DataBLL.GetAllAppointments();
                this.IsLoadingHUD = false;
                if (result.Error == null)
                {
                    Appointments = result.Response.ToObservable();
                }
                else
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = result.Error.Message
                    });
                }
            });
        }

        private void GetRandomUsers(object obj)
        {
            Task.Run(async () => {
                this.LoadingMessageHUD = "Performing download...";
                this.IsLoadingHUD = true;

                var result = await WebBll.GetRandomUsers();

                this.IsLoadingHUD = false;
                if (result.Error == null)
                {
                    RandomUsers = result.Response.ToObservable();
                }
                else
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = result.Error.Message
                    });
                }
            });

        }

        public void BuildResourceList()
        {
            switch (FontType)
            {
                case FontType.FontAwesome:
                    Items = DataBLL.GetFontList(FontAwesome.Icons, FontAwesome.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.EntypoPlus:
                    Items = DataBLL.GetFontList(EntypoPlus.Icons, EntypoPlus.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.Ionicons:
                    Items = DataBLL.GetFontList(Ionicons.Icons, Ionicons.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.Material:
                    Items = DataBLL.GetFontList(Material.Icons, Material.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.Meteocons:
                    Items = DataBLL.GetFontList(Meteocons.Icons, Meteocons.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.SimpleLineIcons:
                    Items = DataBLL.GetFontList(SimpleLineIcons.Icons, SimpleLineIcons.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.Typicons:
                    Items = DataBLL.GetFontList(Typicons.Icons, Typicons.FontFamily).ToObservable<FontItemRow>();
                    break;
                case FontType.WeatherIcons:
                    Items = DataBLL.GetFontList(WeatherIcons.Icons, WeatherIcons.FontFamily).ToObservable<FontItemRow>();
                    break;
            }

        }

        public override void OnViewMessageReceived(string key, object obj)
        {

            switch(key){
                case CoreSettings.LoadResources:
                    Task.Run(async () =>
                    {
                        var errorLogs = await DataBLL.GetLogFiles<ErrorLog>();
                        if (errorLogs.Error == null)
                            ErrorLogs = errorLogs.Response.ToObservable<ErrorLog>();

                        var analyticLogs = await DataBLL.GetLogFiles<AnalyticLog>();
                        if (analyticLogs.Error == null)
                            AnalyticLogs = analyticLogs.Response.ToObservable<AnalyticLog>();
                    });
                    break;
                case CoreSettings.RefreshAppoints:
                    GetDbAppointments(null);
                    break;
            }

        }


    }
}
