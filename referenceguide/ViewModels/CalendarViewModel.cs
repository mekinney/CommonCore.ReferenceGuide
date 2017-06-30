using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class CalendarViewModel : ObservableViewModel
	{
		private Appointment appt;
		private TimeSpan startTime;
		private TimeSpan endTime;

		public TimeSpan StartTime
		{
			get { return startTime; }
			set { SetProperty(ref startTime, value); }
		}

		public TimeSpan EndTime
		{
			get { return endTime; }
			set { SetProperty(ref endTime, value); }
		}

		public Appointment Appt
		{
			get { return appt; }
			set { SetProperty(ref appt, value); }
		}

		public bool DevicePersistOnly { get; set; }


		public ICommand CreateEvent { get; set; }

		public CalendarViewModel()
		{
			Appt = new Appointment();
			CreateEvent = new RelayCommand(async (obj) =>
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

			});
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
			var result = await SqliteDb.AddOrUpdate<Appointment>(this.Appt);
			if (result.Success)
			{
				SendViewMessage<DataExampleViewModel>(AppSettings.RefreshAppoints, this.Appt);
				Appt = new Appointment();
				await this.Navigation.PopAsync(true);
			}
		}
	}
}
