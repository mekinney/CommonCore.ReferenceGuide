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

    public class SimpleViewModel : CoreViewModel
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

            DialogClick = new CoreCommand(DialogClickMethod);
            NotificationClick = new CoreCommand(NotificationClickMethod);
            OverlayClick = new CoreCommand(OverlayClickMethod);
            Blur = new CoreCommand(BlurNewMethod);
            CreateCalendar = new CoreCommand(CreateCalendarMethod);
            PushRegister = new CoreCommand(PushRegisterMethod);
            ShowSnack = new CoreCommand(ShowSnackMethod);
            PlaySound = new CoreCommand(PlaySoundMethod);
            CommTest = new CoreCommand(CommTestMethod);
            ContextMenu = new CoreCommand(ContextMenuMethod);
            BindingTextChanged = new CoreCommand(BindingTextChangedMethod);
            SendSMS = new CoreCommand(SendSMSMethod);
            SendEmail = new CoreCommand(SendEmailMethod);
            MakeCall = new CoreCommand(MakeCallMethod);
            MakeCallEvent = new CoreCommand(MakeCallEventMethod);
            FABClicked = new CoreCommand(FABClickedMethod);
            ClickEvent = new CoreCommand((obj) => { ClickCount++; });

            /*
                This is an example of command validators. ValidateTextFields is part of the CommonCore
                extensions and validators can be chained with more complex rules for numbers and dates 
            */
            CanExecute = new CoreCommand(CanExecuteMethod,
                                          () => { return this.ValidateTextFields(this.FirstName); },
                                          this);
        }

        private void CanExecuteMethod(object obj)
        {
            
        }
        private void FABClickedMethod(object obj)
        {
            DialogPrompt.ShowMessage(new Prompt()
            {
                Title = "FAB Button",
                Message = "The button was clicked"
            });
        }

        private void MakeCallEventMethod(object obj)
        {
            Task.Run(async () =>
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
            });

        }

        private void MakeCallMethod(object obj)
        {
            Task.Run(async () =>
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
            });

        }

        private void SendEmailMethod(object obj)
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

        private void SendSMSMethod(object obj)
        {
            if (!string.IsNullOrEmpty(CommunicationNumber) &&
               !string.IsNullOrEmpty(CommunicationMessage))
                Communication.SendSMS(CommunicationNumber, CommunicationMessage);
        }

        private void BindingTextChangedMethod(object obj)
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

        private void ContextMenuMethod(object obj)
        {
            Navigation.PushNonAwaited<ContextMenuPage>();
        }

        private void CommTestMethod(object obj)
        {
            Navigation.PushNonAwaited<CommunicationPage>();
        }

        private void PlaySoundMethod(object obj)
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

        private void ShowSnackMethod(object obj)
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

        private void PushRegisterMethod(object obj)
        {
            AzureNotificationHub.RegisterNotificationHub();
            PushButtonLabel = "** Notifications Registered **";
        }

        private void CreateCalendarMethod(object obj)
        {
            Navigation.PushNonAwaited(new CalendarEventPage() { DevicePersistOnly = true });
        }

        private void BlurNewMethod(object obj)
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

        private void OverlayClickMethod(object obj)
        {
            Task.Run(async () =>
            {
                LoadingMessageOverlay = "Loading Overlay Data...";
                IsLoadingOverlay = true;
                await Task.Delay(2000);
                IsLoadingOverlay = false;
            });

        }

        private void NotificationClickMethod(object obj)
        {
            this.ShowNotification(new LocalNotification()
            {
                Id = 1,
                Title = "Test",
                Message = "This is just a message"
            });
        }

        private void DialogClickMethod(object obj)
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


