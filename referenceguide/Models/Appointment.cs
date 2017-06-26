using System;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{

	public class Appointment : DataModel
	{
		private string title;
		private string description;
		private string location;
		private DateTime startDate;
		private DateTime endDate;
		private bool hasReminder;

		public string ID { get; set; }

		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
		}

		public string Location
		{
			get { return location; }
			set { SetProperty(ref location, value); }
		}

		public DateTime StartDate
		{
			get { return startDate; }
			set { SetProperty(ref startDate, value); }
		}

		public DateTime EndDate
		{
			get { return endDate; }
			set { SetProperty(ref endDate, value); }
		}

		public bool HasReminder
		{
			get { return hasReminder; }
			set { SetProperty(ref hasReminder, value); }
		}

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
