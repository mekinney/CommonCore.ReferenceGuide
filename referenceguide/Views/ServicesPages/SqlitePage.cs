using System;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AppointmentCell : ViewCell
    {
        private readonly CachedImage img;
        private readonly Label lblTitle;
        private readonly Label lblFullDisplay;
        private MenuItem embeddedMenu;
        private MenuItem deleteMenu;

        public AppointmentCell()
        {
            this.Height = 65;
            img = new CachedImage()
            {
                Margin = new Thickness(8, 0, 4, 0),
                HeightRequest = 32,
                WidthRequest = 32,
                DownsampleWidth = 32,
                DownsampleHeight = 32,
                DownsampleUseDipUnits = true,
                RetryCount = 0,
                RetryDelay = 250,
                LoadingPlaceholder = "placeholder.png",
                CacheDuration = TimeSpan.FromDays(10),
                Source = "calendar.png"
            };

            lblTitle = new Label()
            {
                Margin = new Thickness(5, 5, 5, -5),
            };
          

            lblFullDisplay = new Label()
            {
                Style = AppStyles.AddressCell,
            };

            var rightPanel = new StackLayout()
            {
                Padding = 0,
                Children = { lblTitle, lblFullDisplay }
            };

            embeddedMenu = new MenuItem()
            {
                Text = "Embedded",
                IsDestructive = false
            };
            embeddedMenu.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            ContextActions.Add(embeddedMenu);

            deleteMenu = new MenuItem()
            {
                Text = "Delete",
                IsDestructive = true,
                AutomationId = "deleteMenu",
                Command = new Command(async (obj) =>
                {
                    var appt = (Appointment)obj;
                    appt.MarkedForDelete = true;
                    var sqlite = InjectionManager.GetService<ISqliteDb, SqliteDb>();
                    var result = await sqlite.AddOrUpdate<Appointment>(appt);
                    if (result.Success)
                        InjectionManager.SendViewModelMessage<DataExampleViewModel>(AppSettings.RefreshAppoints, null);

                })
            };
            deleteMenu.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            ContextActions.Add(deleteMenu);

            View = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { img, rightPanel }
            };
        }

        //On a listview that uses RecycleElement binding can be costly
        protected override void OnBindingContextChanged()
        {
            var item = ((Appointment)BindingContext);
            lblTitle.Text = item.Title;
            lblFullDisplay.FormattedText = item.FormattedDisplay;
            base.OnBindingContextChanged();
        }
    }
    public class SqlitePage : BoundPage<DataExampleViewModel>
    {
        public SqlitePage()
        {
            this.Title = "Sqlite Events";

            VM.GetDbAppointments(null);

            this.ToolbarItems.Add(new ToolbarItem("Create", null, async () =>
            {
                await Navigation.PushAsync(new CalendarEventPage() { DevicePersistOnly = false });
            })
            {
                AutomationId = "Create"
            });


            var lstView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(AppointmentCell)),
                AutomationId = "lstView"
            };
            lstView.SetBinding(ListView.ItemsSourceProperty, "Appointments");


            Content = new StackLayout()
            {
                Children = { lstView }
            };

        }
    }
}
