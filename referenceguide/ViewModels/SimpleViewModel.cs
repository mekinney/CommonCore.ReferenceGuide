using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public enum AudioState
    {
        Stopped,
        Paused,
        Playing
    }
    public class State
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
	public class SimpleViewModel : ObservableViewModel
	{
		private string firstName;
		private string pushButtonLabel;
        private long phoneNumber;
        private int selectedRank;
        private string selectedRankText;
        private IAudioPlayer audioplayer;
		private ObservableCollection<State> states;
        private AudioState playingState;

        public int SelectedRank
        {
			get { return selectedRank; }
			set 
            { 
                SetProperty(ref selectedRank, value);
                var plural = value == 1 ? "star" : "stars";
                SelectedRankText = $"You selected {value} {plural}";
            }
        }
        public string SelectedRankText
        {
			get { return selectedRankText; }
			set { SetProperty(ref selectedRankText, value); }
        }
        public long PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }
		public string FirstName
		{
			get { return firstName; }
			set { SetProperty(ref firstName, value); }

		}
		public ObservableCollection<State> States
		{
			get { return states ?? (states = new ObservableCollection<State>()); }
			set { SetProperty(ref states, value); }
		}

		public string PushButtonLabel
		{
			get { return pushButtonLabel; }
			set { SetProperty(ref pushButtonLabel, value); }
		}


		public ICommand DialogClick { get; set; }
		public ICommand NotificationClick { get; set; }
		public ICommand OverlayClick { get; set; }
		public ICommand Blur { get; set; }
		public ICommand CreateCalendar { get; set; }
		public ICommand PushRegister { get; set; }
        public ICommand ShowSnack { get; set; }
        public ICommand PlaySound { get; set; }
        public ICommand CommTest { get; set; }
        public ICommand ContextMenu { get; set; }

		public SimpleViewModel()
		{
			PushButtonLabel = "Register Push Notification";

            var lst = new List<State>(new State[] {
                    new State { Value = "AL", Text = "Alabama" },
                    new State { Value = "AK", Text = "Alaska" },
                    new State { Value = "AZ", Text = "Arizona" },
                    new State { Value = "AR", Text = "Arkansas" },
                    new State { Value = "CA", Text = "California" },
                    new State { Value = "CO", Text = "Colorado" },
                    new State { Value = "CT", Text = "Connecticut" },
                    new State { Value = "DE", Text = "Delaware" },
                    new State { Value = "FL", Text = "Florida" },
                    new State { Value = "GA", Text = "Georgia" },
                    new State { Value = "HI", Text = "Hawaii" },
                    new State { Value = "ID", Text = "Idaho" },
                    new State { Value = "IL", Text = "Illinois" },
                    new State { Value = "IN", Text = "Indiana" },
                    new State { Value = "IA", Text = "Iowa" },
                    new State { Value = "KS", Text = "Kansas" },
                    new State { Value = "KY", Text = "Kentucky" },
                    new State { Value = "LA", Text = "Louisiana" },
                    new State { Value = "ME", Text = "Maine" },
                    new State { Value = "MD", Text = "Maryland" },
                    new State { Value = "MA", Text = "Massachusetts" },
                    new State { Value = "MI", Text = "Michigan" },
                    new State { Value = "MN", Text = "Minnesota" },
                    new State { Value = "MS", Text = "Mississippi" },
                    new State { Value = "MO", Text = "Missouri" },
                    new State { Value = "MT", Text = "Montana" },
                    new State { Value = "NC", Text = "North Carolina" },
                    new State { Value = "ND", Text = "North Dakota" },
                    new State { Value = "NE", Text = "Nebraska" },
                    new State { Value = "NV", Text = "Nevada" },
                    new State { Value = "NH", Text = "New Hampshire" },
                    new State { Value = "NJ", Text = "New Jersey" },
                    new State { Value = "NM", Text = "New Mexico" },
                    new State { Value = "NY", Text = "New York" },
                    new State { Value = "OH", Text = "Ohio" },
                    new State { Value = "OK", Text = "Oklahoma" },
                    new State { Value = "OR", Text = "Oregon" },
                    new State { Value = "PA", Text = "Pennsylvania" },
                    new State { Value = "RI", Text = "Rhode Island" },
                    new State { Value = "SC", Text = "South Carolina" },
                    new State { Value = "SD", Text = "South Dakota" },
                    new State { Value = "TN", Text = "Tennessee" },
                    new State { Value = "TX", Text = "Texas" },
                    new State { Value = "UT", Text = "Utah" },
                    new State { Value = "VT", Text = "Vermont" },
                    new State { Value = "VA", Text = "Virginia" },
                    new State { Value = "WA", Text = "Washington" },
                    new State { Value = "WV", Text = "West Virginia" },
                    new State { Value = "WI", Text = "Wisconsin" },
                    new State { Value = "WY", Text = "Wyoming" }
            });

            States = lst.ToObservable();
			

			DialogClick = new RelayCommand((obj) =>
			{
				DialogPrompt.ShowMessage(new Prompt()
				{
					Title = "Test",
					Message = "This is just a message"
				});
			});

			NotificationClick = new RelayCommand((obj) =>
			{
				this.ShowNotification(new LocalNotification()
				{
					Id = 1,
					Title = "Test",
					Message = "This is just a message"
				});


			});

			OverlayClick = new RelayCommand(async (obj) =>
			{
				LoadingMessageOverlay = "Loading Overlay Data...";
				IsLoadingOverlay = true;
				await Task.Delay(2000);
				IsLoadingOverlay = false;
			});

			Blur = new RelayCommand((obj) =>
			{
				BlurOverlay.Show();

				Device.BeginInvokeOnMainThread(() =>
				{
					DialogPrompt.ShowMessage(new Prompt()
					{
						Title = "Blurred Background",
						Message = "Click okay to close",
						Callback = (result) =>
						{
							BlurOverlay.Hide();
						}
					});

				});

			});

			CreateCalendar = new RelayCommand(async (obj) =>
			{
				await this.Navigation.PushAsync(new CalendarEventPage() { DevicePersistOnly = true });
			});

			PushRegister = new RelayCommand((obj) =>
			{
				var di = DeviceInfo.GetDeviceInformation();

				if (Device.RuntimePlatform.ToUpper() == "IOS" &&
				   di.DeviceType == DeviceState.Simulator)
				{
					DialogPrompt.ShowMessage(new Prompt()
					{
						Title = "Simulator",
						Message = "Push Notifications Unavailable"
					});
				}
				else
				{
					AzureNotificationHub.RegisterNotificationHub();
					PushButtonLabel = "** Notifications Registered **";
				}

			});

            ShowSnack = new RelayCommand((obj) => {

                SnackBar.Show(new Snack()
                {
                    Duration = 10000,
                    Background = Color.FromHex("#DF8049"),
                    TextColor=Color.White,
                    ActionTextColor=Color.White,
                    Text = "Excellent Work!!",
                    ActionText = "Ok",
                    Action = (aobj) => {
                        SnackBar.Close();
                    }
                });
            });

            PlaySound = new RelayCommand((obj) => {
                if(AudioPlayer.OnFinishedPlaying==null)
					AudioPlayer.OnFinishedPlaying = () =>
					{
                        playingState = AudioState.Stopped;
					};

                switch (playingState)
                {
                    case AudioState.Stopped:
                        playingState = AudioState.Playing;
                        AudioPlayer.Play("hailchief.mp3");
                        break;
                    case AudioState.Paused:
                        playingState = AudioState.Playing;
                        AudioPlayer.Play();
                        break;
                    default:
                        playingState = AudioState.Paused;
                        AudioPlayer.Pause();
                        break;

                }

            });

            CommTest = new RelayCommand(async(obj) => {
                await this.Navigation.PushAsync(new CommunicationPage());
            });
			ContextMenu = new RelayCommand(async (obj) =>
			{
				await this.Navigation.PushAsync(new ContextMenuPage());
			});
		}

		public void DisplayNotification(LocalNotification note)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				this.ShowNotification(note);
			});
		}

        public override void OnViewMessageReceived(string key, object obj)
        {
            if(key=="PhoneCallBack"){
				Device.BeginInvokeOnMainThread(() =>
				{
					DialogPrompt.ShowMessage(new Prompt()
					{
						Title = "Completed",
						Message = "Phone call action has been complete and action logged"
					});
				});
            }
        }

	}
}


