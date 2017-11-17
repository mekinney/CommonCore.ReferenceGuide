using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CalendarEventPage : CorePage<CalendarViewModel>
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

            evtNameEntry = new CoreUnderlineEntry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                EntryColor = Color.DarkGray, 
            };
            evtNameEntry.SetBinding(Entry.TextProperty, new Binding(path:"Appt.Title", mode:BindingMode.TwoWay));

            var evtDescription = new Label()
            {
                TextColor = Color.Gray,
                Text = "Description",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1),
                AutomationId = "evtDescription"
            };

            var evtDescriptionEntry = new CoreUnderlineEntry()
            {
                Margin = new Thickness(5, 1, 5, 1),
                EntryColor = Color.DarkGray,
                AutomationId = "evtDescriptionEntry"
            };
            evtDescriptionEntry.SetBinding(Entry.TextProperty, "Appt.Description");

            var calendarSelect = CreateCalendarSelectionPanel();
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

            Content = new CompressedStackLayout()
            {
                Padding = 15,
                Children = 
                { 
                    lblExplain, 
                    evtName, 
                    evtNameEntry, 
                    evtDescription, 
                    evtDescriptionEntry, 
                    calendarSelect, 
                    startTime, 
                    endTime, 
                    evtHasReminder, 
                    swReminder, 
                    btnCreate 
                }
            };

            this.SetAutomationIds();
        }

        string GetVariableName<T>(Expression<Func<T>> expr)
        {
            var body = (MemberExpression)expr.Body;

            return body.Member.Name;
        }

        private CompressedStackLayout CreateCalendarSelectionPanel()
        {
            var calendarSelect = new Label()
            {
                TextColor = Color.Gray,
                Text = "Select Calendar",
                FontSize = 14,
                Margin = new Thickness(5, 5, 5, 1)
            };

            var calendarPicker = new CorePicker()
            {
                Margin = new Thickness(5, 1, 5, 1),
                AutomationId = "calendarPicker",
                BindingPath="DisplayName", 
                EntryColor = Color.DarkGray 
            };
            calendarPicker.SetBinding(CorePicker.ItemsSourceProperty, "DeviceCalendars");
            calendarPicker.SetBinding(CorePicker.SelectedItemProperty,"SelectedDeviceCalendar");

            return new CompressedStackLayout()
            {
                Children = { calendarSelect, calendarPicker }
            };
        }

        private CompressedStackLayout CreateStartDateTimePanel()
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
                AutomationId = "evtStartDateEntry",

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

            var startDateTimePanel = new CompressedStackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { startDatePanel, startTimePanel }
            };

            return startDateTimePanel;
        }


        private CompressedStackLayout CreateEndDateTimePanel()
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

            var endDateTimePanel = new CompressedStackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { endDatePanel, endTimePanel }
            };

            return endDateTimePanel;
        }
    }
}
