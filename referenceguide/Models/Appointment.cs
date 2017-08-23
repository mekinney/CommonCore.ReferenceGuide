using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{

	public class Appointment : SqlDataModel
	{
        
		public string ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
		public string Location{ get; set; }

		public DateTime StartDate{ get; set; }

		public DateTime EndDate{ get; set; }

		public bool HasReminder{ get; set; }

		public Appointment()
		{
			StartDate = DateTime.Now;
			EndDate = DateTime.Now;
		}

		public FormattedString FormattedDisplay
		{
			get
			{
				var fs = new FormattedString();
				var span2 = new Span() { Text = $"{StartDate.ToShortDateString()} at {StartDate.ToShortTimeString()} \n" };
				var span3 = new Span() { Text = $"{EndDate.ToShortDateString()} at {EndDate.ToShortTimeString()} \n" };
				fs.Spans.Add(span2);
				fs.Spans.Add(span3);
				return fs;
			}
		}

	}

	public static class AppointmentExtensionClass
	{
		public static CalendarEventModel ToCalendarEvent(this Appointment appt)
		{
			var evt = new CalendarEventModel()
			{
				Description = appt.Description,
				EndTime = appt.EndDate,
				HasReminder = appt.HasReminder,
				Location = appt.Location,
				StartTime = appt.StartDate,
				Title = appt.Title
			};
			return evt;
		}
	}
}
