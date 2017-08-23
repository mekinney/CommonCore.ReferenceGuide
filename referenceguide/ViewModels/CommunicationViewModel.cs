using System;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class CommunicationViewModel : ObservableViewModel
    {
        
		public string CommunicationEmail{ get; set; }
        public string CommunicationNumber{ get; set; }
        public string CommunicationMessage{ get; set; }

        public ICommand SendSMS { get; set; }
        public ICommand SendEmail { get; set; }
		public ICommand MakeCall { get; set; }
		public ICommand MakeCallEvent { get; set; }

        public CommunicationViewModel()
        {
            SendSMS = new RelayCommand((obj) =>
            {
                if (!string.IsNullOrEmpty(CommunicationNumber) && 
                   !string.IsNullOrEmpty(CommunicationMessage))
                    Communication.SendSMS(CommunicationNumber, CommunicationMessage);

            });
            SendEmail = new RelayCommand((obj) =>
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
            });

			MakeCall = new RelayCommand(async (obj) =>
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

			MakeCallEvent = new RelayCommand(async (obj) =>
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
    }
}
