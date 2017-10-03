using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class CalendarViewModel : ObservableViewModel
	{
		public TimeSpan StartTime{ get; set; }
		public TimeSpan EndTime{ get; set; }
		public Appointment Appt{ get; set; }
		public bool DevicePersistOnly { get; set; }

        public ICommand CreateEvent { get; set; } 

		public CalendarViewModel()
		{
			Appt = new Appointment();
            CreateEvent = new RelayCommand(async (obj) => { await CreateEventMethod(); });
		}

        private async Task CreateEventMethod()
        {
			Appt.StartDate = new DateTime(Appt.StartDate.Year, Appt.StartDate.Month, Appt.StartDate.Day).Add(StartTime);

			Appt.EndDate = new DateTime(Appt.EndDate.Year, Appt.EndDate.Month, Appt.EndDate.Day).Add(EndTime);

			if (DevicePersistOnly)
			{
				SaveToPhone();
			}
			else
			{
				await SaveToDatabase();
			}
        }


		private void SaveToPhone()
		{
			var evt = Appt.ToCalendarEvent();

			CalendarEvent.CreateCalendarEvent(evt, (obj) =>
			{
				Appt = new Appointment();
				DialogPrompt.ShowMessage(new Prompt()
				{
					Title = "Device Calendar",
					Message = $"The save event result was {obj}"
				});

			});

		}
		private async Task SaveToDatabase()
		{
            var result = await DataBLL.SaveAppointment(this.Appt);
			if (result.Error==null)
			{
				SendViewMessage<DataExampleViewModel>(AppSettings.RefreshAppoints, this.Appt);
				Appt = new Appointment();
                Navigation.PopNonAwaited();
			}
		}
	}
}
