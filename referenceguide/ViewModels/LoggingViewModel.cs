using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;
using System.Linq;
namespace referenceguide
{
    public class LoggingViewModel : ObservableViewModel
    {
        private ObservableCollection<ErrorLog> errorLogs;
        private ObservableCollection<AnalyticLog> analyticLogs;

        public ObservableCollection<ErrorLog> ErrorLogs
        {
            get { return errorLogs ?? (errorLogs = new ObservableCollection<ErrorLog>()); }
            set { SetProperty(ref errorLogs, value); }
        }

		public ObservableCollection<AnalyticLog> AnalyticLogs
		{
			get { return analyticLogs ?? (analyticLogs = new ObservableCollection<AnalyticLog>()); }
			set { SetProperty(ref analyticLogs, value); }
		}

        public ICommand CreateErrorEntry { get; set; }
        public ICommand ClearErrorEntries { get; set; }
		public ICommand ClearAnalyticEntries { get; set; }

        public LoggingViewModel()
        {
			ErrorLogs = new ObservableCollection<ErrorLog>();
            AnalyticLogs = new ObservableCollection<AnalyticLog>();

            CreateErrorEntry = new RelayCommand(async(obj) => {
                PerformErrorLogic();
                var result =  await Log.GetHistoricalLogs(LogType.Error);
                ErrorLogs = result.ToObservable<ErrorLog>();
            });
            ClearErrorEntries = new RelayCommand(async(obj) => { 
                await Log.ClearLogging(LogType.Error);
				ErrorLogs = new ObservableCollection<ErrorLog>();
            });
			ClearAnalyticEntries = new RelayCommand(async(obj) =>
			{
                await Log.ClearLogging(LogType.Analytic);
                AnalyticLogs = new ObservableCollection<AnalyticLog>();
			});
        }

        public override void LoadResources(string parameter = null)
        {
			Log.GetHistoricalLogs(LogType.Analytic).ContinueWith((x) =>
			{
				if (x.Result != null)
				{
                    Device.BeginInvokeOnMainThread(() => { 
                        AnalyticLogs = x.Result.ToObservable<AnalyticLog>();
                    });
					
				}
			});
			Log.GetHistoricalLogs(LogType.Error).ContinueWith((x) =>
			{
				if (x.Result != null)
				{
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ErrorLogs = x.Result.ToObservable<ErrorLog>();
                    });
				}
			});
        }

        private void PerformErrorLogic()
        {
            try
            {
                var x = 10;
                var y = 0;
                var total = x / y;
                Console.WriteLine($"The total is {total}");
            }
            catch (Exception ex)
            {
                ex.ConsoleWrite(true);
                Log.LogException(ex);
            }
        }

    }
}
