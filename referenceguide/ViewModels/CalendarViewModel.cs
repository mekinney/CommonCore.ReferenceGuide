﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
	public class CalendarViewModel : CoreViewModel
	{
		public TimeSpan StartTime{ get; set; }
		public TimeSpan EndTime{ get; set; }
		public Appointment Appt{ get; set; }
		public bool DevicePersistOnly { get; set; }
        public ObservableCollection<CalendarAccount> DeviceCalendars { get; set; }
        public CalendarAccount SelectedDeviceCalendar { get; set; }

        public ICommand CreateEvent { get; set; } 

		public CalendarViewModel()
		{
			Appt = new Appointment();
            CreateEvent = new CoreCommand(CreateEventMethod);
		}

        private void CreateEventMethod(object obj)
        {
            Task.Run(async () =>
            {
                
                Appt.StartDate = new DateTime(Appt.StartDate.Year, Appt.StartDate.Month, Appt.StartDate.Day).Add(StartTime);
                Appt.EndDate = new DateTime(Appt.EndDate.Year, Appt.EndDate.Month, Appt.EndDate.Day).Add(EndTime);

                if (DevicePersistOnly)
                {
                    await SaveToPhone();
                }
                else
                {
                    await SaveToDatabase();
                }
            });
        }


		private async Task SaveToPhone()
		{
            if (SelectedDeviceCalendar != null)
            {
                var evt = Appt.ToCalendarEvent();
                evt.DeviceCalendar = SelectedDeviceCalendar;
                var response = await CalendarEvent.CreateCalendarEvent(evt);
                if (response.result)
                {
                    try
                    {
                       
                            var id = response.model.Id;
                            var c = await CalendarEvent.GetCalendarEvent(id);
                            c.StartTime = c.StartTime.AddHours(2);
                            c.EndTime = c.EndTime.AddHours(2);
                            c.ReminderMinutes = 90;
                            c.Description = "hello kitty";
                            var updateResponse = await CalendarEvent.UpdateCalendarEvent(c);
                            if (updateResponse.result)
                            {
                                var x = "success update";
                            }
                     
 
                    }
                    catch (Exception ex)
                    {
                        var excep = ex;
                    }
 
                    Appt = new Appointment();
                }
                else
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Calendar Event Failed",
                        Message = "There was an issues saving the calendar event"
                    });
                }
            }


		}
		private async Task SaveToDatabase()
		{
            var result = await DataBLL.SaveAppointment(this.Appt);
			if (result.Error==null)
			{
                SendViewMessage<DataExampleViewModel>(CoreSettings.RefreshAppoints, this.Appt);
				Appt = new Appointment();

                Device.BeginInvokeOnMainThread(async()=>{
                    await CoreSettings.AppNav.PopAsync();
                });

			}
		}

        public override void OnViewMessageReceived(string key, object obj)
        {
            switch(key){
                case CoreSettings.LoadResources:
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                        var result = await CalendarEvent.GetCalendars();
                        DeviceCalendars = result?.ToObservable<CalendarAccount>();
                    });
                    break;
            }
        }
    }
}
