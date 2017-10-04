using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
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
        private int selectedRank;
        private AudioState playingState;

        public int SelectedRank
        {
            get { return selectedRank; }
            set
            {

                selectedRank = value;
                OnPropertyChanged("SelectedRank");
                var plural = value == 1 ? "star" : "stars";
                SelectedRankText = $"You selected {value} {plural}";
            }
        }

        public string CommunicationEmail { get; set; }
        public string CommunicationNumber { get; set; }
        public string CommunicationMessage { get; set; }
        public string SelectedRankText { get; set; }
        public long PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public int SelectedRadioIndex { get; set; } = -1;
        public string PushButtonLabel { get; set; }
        public string BindingTextValue { get; set; }
        public int ClickCount { get; set; }

        public ObservableCollection<State> States { get; set; } = new ObservableCollection<State>();
        public ObservableCollection<string> RadioOptions { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<CarouselBindingObject> ItemSource { get; set; }

        public ICommand ClickEvent { get; set; }
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
        public ICommand BindingTextChanged { get; set; }
        public ICommand FABClicked { get; set; }
        public ICommand SendSMS { get; set; }
        public ICommand SendEmail { get; set; }
        public ICommand MakeCall { get; set; }
        public ICommand MakeCallEvent { get; set; }
        public ICommand CanExecute { get; set; }


        public SimpleViewModel()
        {
            PushButtonLabel = "Register Push Notification";

            var radioOptions = new List<string>(
                new string[] { "Blue", "Red", "Green" }
            );
            RadioOptions = radioOptions.ToObservable<string>();

            var stateResults = DataBLL.GetAllStates();
            if (stateResults.Error == null)
                States = stateResults.Response.ToObservable();

            DialogClick = new RelayCommand((obj) => { DialogClickMethod(); });
            NotificationClick = new RelayCommand((obj) => { NotificationClickMethod(); });
            OverlayClick = new RelayCommand(async (obj) => { await OverlayClickMethod(); });
            Blur = new RelayCommand((obj) => { BlurNewMethod(); });
            CreateCalendar = new RelayCommand((obj) => { CreateCalendarMethod(); });
            PushRegister = new RelayCommand((obj) => { PushRegisterMethod(); });
            ShowSnack = new RelayCommand((obj) => { ShowSnackMethod(); });
            PlaySound = new RelayCommand((obj) => { PlaySoundMethod(); });
            CommTest = new RelayCommand((obj) => { CommTestMethod(); });
            ContextMenu = new RelayCommand((obj) => { ContextMenuMethod(); });
            BindingTextChanged = new RelayCommand((obj) => { BindingTextChangedMethod(); });
            SendSMS = new RelayCommand((obj) => { SendSMSMethod(); });
            SendEmail = new RelayCommand((obj) => { SendEmailMethod(); });
            MakeCall = new RelayCommand(async (obj) => { await MakeCallMethod(); });
            MakeCallEvent = new RelayCommand(async (obj) => { await MakeCallEventMethod(); });
            FABClicked = new RelayCommand((obj) => { FABClickedMethod(); });
            ClickEvent = new RelayCommand((obj) => { ClickCount++; });

            CanExecute = new RelayCommand((obj) => { CanExecuteMethod(); }, 
                                          () => this.ValidateTextFields(this.FirstName), 
                                          this);
        }

        private void CanExecuteMethod()
        {
            
        }
        private void FABClickedMethod()
        {
            DialogPrompt.ShowMessage(new Prompt()
            {
                Title = "FAB Button",
                Message = "The button was clicked"
            });
        }

        private async Task MakeCallEventMethod()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Phone))
                    {
                        DialogPrompt.ShowMessage(new Prompt()
                        {
                            Title = "Permission",
                            Message = "The application needs access to the phone."
                        });
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Phone });
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    Communication.PlaceCallWithCallBack(CommunicationNumber.ToString(), "PhoneCallBack");
                }
                else if (status != PermissionStatus.Unknown)
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Issue",
                        Message = "There was a problem accessing the phone."
                    });
                }
            }
            catch (Exception ex)
            {

                DialogPrompt.ShowMessage(new Prompt()
                {
                    Title = "Error",
                    Message = "The application experience an error accessing the phone."
                });
            }
        }

        private async Task MakeCallMethod()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Phone))
                    {
                        DialogPrompt.ShowMessage(new Prompt()
                        {
                            Title = "Permission",
                            Message = "The application needs access to the phone."
                        });
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Phone });
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    Communication.PlaceCall(CommunicationNumber.ToString());
                }
                else if (status != PermissionStatus.Unknown)
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Issue",
                        Message = "There was a problem accessing the phone."
                    });
                }
            }
            catch (Exception ex)
            {

                DialogPrompt.ShowMessage(new Prompt()
                {
                    Title = "Error",
                    Message = "The application experience an error accessing the phone."
                });
            }
        }

        private void SendEmailMethod()
        {
            if (!string.IsNullOrEmpty(CommunicationEmail) &&
                !string.IsNullOrEmpty(CommunicationMessage))
                Communication.SendEmail(new EmailMessage()
                {
                    EmailAddress = CommunicationEmail,
                    Message = CommunicationMessage,
                    Subject = "Email Test",
                    Title = "Mobile Email"
                });
        }

        private void SendSMSMethod()
        {
            if (!string.IsNullOrEmpty(CommunicationNumber) &&
               !string.IsNullOrEmpty(CommunicationMessage))
                Communication.SendSMS(CommunicationNumber, CommunicationMessage);
        }

        private void BindingTextChangedMethod()
        {
            if (!string.IsNullOrEmpty(BindingTextValue) && BindingTextValue.Length > 2)
            {
                int num;
                var valid = int.TryParse(BindingTextValue, out num);
                if (!valid)
                {
                    DialogPrompt.ShowMessage(new Prompt()
                    {
                        Title = "Error",
                        Message = "Can't you even follow directions?"
                    });
                }
            }
        }

        private void ContextMenuMethod()
        {
            Navigation.PushNonAwaited<ContextMenuPage>();
        }

        private void CommTestMethod()
        {
            Navigation.PushNonAwaited<CommunicationPage>();
        }

        private void PlaySoundMethod()
        {
            if (AudioPlayer.OnFinishedPlaying == null)
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
        }

        private void ShowSnackMethod()
        {
            SnackBar.Show(new Snack()
            {
                Duration = 10000,
                Background = Color.FromHex("#DF8049"),
                TextColor = Color.White,
                ActionTextColor = Color.White,
                Text = "Excellent Work!!",
                ActionText = "Ok",
                Action = (aobj) =>
                {
                    SnackBar.Close();
                }
            });
        }

        private void PushRegisterMethod()
        {
            AzureNotificationHub.RegisterNotificationHub();
            PushButtonLabel = "** Notifications Registered **";
        }

        private void CreateCalendarMethod()
        {
            Navigation.PushNonAwaited(new CalendarEventPage() { DevicePersistOnly = true });
        }

        private void BlurNewMethod()
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
        }

        private async Task OverlayClickMethod()
        {
            LoadingMessageOverlay = "Loading Overlay Data...";
            IsLoadingOverlay = true;
            await Task.Delay(2000);
            IsLoadingOverlay = false;
        }

        private void NotificationClickMethod()
        {
            this.ShowNotification(new LocalNotification()
            {
                Id = 1,
                Title = "Test",
                Message = "This is just a message"
            });
        }

        private void DialogClickMethod()
        {
            DialogPrompt.ShowMessage(new Prompt()
            {
                Title = "Test",
                Message = "This is just a message"
            });
        }

        public override void LoadResources(string parameter = null)
        {

            var result = DataBLL.GetCarouselData();
            if (result.Error == null)
                ItemSource = result.Response.ToObservable<CarouselBindingObject>();
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
            if (key == "PhoneCallBack")
            {
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


