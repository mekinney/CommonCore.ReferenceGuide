using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CalendarEventPage : BoundPage<CalendarViewModel>
    {
        private Entry evtNameEntry;

        public bool DevicePersistOnly
        {
            get { return VM.DevicePersistOnly; }
            set { VM.DevicePersistOnly = value; }
        }
        public CalendarEventPage()
        {
            this.Title = "Calendar Wizard";

            var lblExplain = new Label()
            {
                Text = "Please enter the required fields to create a calendar event."
            };

            var evtName = new Label()
            {
                TextColor = Color.Gray,
                Text = "Event Name",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            evtNameEntry = new Entry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                //AutomationId = "evtNameEntry"
            };
            evtNameEntry.SetBinding(Entry.TextProperty, new Binding("Appt.Title", BindingMode.TwoWay));

            var evtDescription = new Label()
            {
                TextColor = Color.Gray,
                Text = "Description",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1),
                AutomationId = "evtDescription"
            };

            var evtDescriptionEntry = new Entry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "evtDescriptionEntry"
            };
            evtDescriptionEntry.SetBinding(Entry.TextProperty, "Appt.Description");

            var startTime = CreateStartDateTimePanel();
            var endTime = CreateEndDateTimePanel();

            var evtHasReminder = new Label()
            {
                TextColor = Color.Gray,
                Text = "Has Reminder",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };
            var swReminder = new Switch()
            {
                Margin = 5,
                AutomationId = "swReminder"
            };
            swReminder.SetBinding(Switch.IsToggledProperty, "Appt.HasReminder");

            var btnCreate = new CoreButton()
            {
                Text = "Create Event",
                Style = AppStyles.LightOrange,
                Margin = 5,
                AutomationId = "btnCreate"
            };
            btnCreate.SetBinding(CoreButton.CommandProperty, "CreateEvent");

            Content = new StackLayout()
            {
                Padding = 15,
                Children = { lblExplain, evtName, evtNameEntry, evtDescription, evtDescriptionEntry, startTime, endTime, evtHasReminder, swReminder, btnCreate }
            };

            this.SetAutomationIds();
        }

        string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;

            return body.Member.Name;
        }

        private StackLayout CreateStartDateTimePanel()
        {
            var evtStartDate = new Label()
            {
                TextColor = Color.Gray,
                Text = "Start Date",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var evtStartDateEntry = new DatePicker()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "evtStartDateEntry"
            };
            evtStartDateEntry.SetBinding(DatePicker.DateProperty, "Appt.StartDate");

            var startDatePanel = new StackLayout()
            {
                Children = { evtStartDate, evtStartDateEntry }
            };

            var evtStartTime = new Label()
            {
                TextColor = Color.Gray,
                Text = "Start Time",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var evtStartTimeEntry = new TimePicker()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "evtStartTimeEntry"
            };
            evtStartTimeEntry.SetBinding(TimePicker.TimeProperty, "StartTime");

            var startTimePanel = new StackLayout()
            {
                Children = { evtStartTime, evtStartTimeEntry }
            };

            var startDateTimePanel = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { startDatePanel, startTimePanel }
            };

            return startDateTimePanel;
        }


        private StackLayout CreateEndDateTimePanel()
        {
            var evtEndDate = new Label()
            {
                TextColor = Color.Gray,
                Text = "End Date",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var evtEndDateEntry = new DatePicker()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "evtEndDateEntry"
            };
            evtEndDateEntry.SetBinding(DatePicker.DateProperty, "Appt.EndDate");

            var endDatePanel = new StackLayout()
            {
                Children = { evtEndDate, evtEndDateEntry }
            };

            var evtEndTime = new Label()
            {
                TextColor = Color.Gray,
                Text = "End Time",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var evtEndTimeEntry = new TimePicker()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "evtEndTimeEntry"
            };
            evtEndTimeEntry.SetBinding(TimePicker.TimeProperty, "EndTime");

            var endTimePanel = new StackLayout()
            {
                Children = { evtEndTime, evtEndTimeEntry }
            };

            var endDateTimePanel = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { endDatePanel, endTimePanel }
            };

            return endDateTimePanel;
        }
    }
}
