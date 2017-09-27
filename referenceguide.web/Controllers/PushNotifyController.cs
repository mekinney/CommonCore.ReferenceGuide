using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Azure.NotificationHubs;
using Newtonsoft.Json;

namespace referenceguide.web.Controllers
{
    public class PushMessage
    {
        public int DeviceType { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Tag { get; set; }
        public bool Success { get; set; }
    }
    public class PushNotifyController : ApiController
    {
        private NotificationHubClient hub;
		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.Route("PushNotify/NotifyHub")]
		public IHttpActionResult NotifyHub(PushMessage msg)
		{
            try
            {
				var connStr = "Endpoint=sb://azdevelop.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=XquAb+oTzm4huD0htIMeV+mCG/o8ekm6958RTPvYCpQ=";
				var hubName = "referenceguide";
				hub = NotificationHubClient.CreateClientFromConnectionString(connStr, hubName);
				NotificationOutcome outcome = null;
				if (msg.DeviceType == 1)
				{
					outcome = SendAPNS(msg);
				}
				else
				{
					outcome = SendGCM(msg);
				}
				msg.Success = outcome.State == NotificationOutcomeState.Completed ? true : false;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

			return Ok(msg);
		}


        private NotificationOutcome SendGCM(PushMessage msg)
        {
            var obj = new GCMMessage.RootObject();
            obj.data.Add("title",msg.Title);
            obj.data.Add("message", msg.Message);

            var json = JsonConvert.SerializeObject(obj);
			return hub.SendGcmNativeNotificationAsync(json, msg.Tag).Result;
        }

		private NotificationOutcome SendAPNS(PushMessage msg)
		{
            var obj = new APNS.RootObject();
            obj.Content.Alert.Add("title","Special Alert");
            obj.Content.Alert.Add("body", "There is a sale at Walmart");
            obj.Content.Badge = 1;
            obj.Content.ContentAvailable = 1;

            var json = JsonConvert.SerializeObject(obj);
            return hub.SendAppleNativeNotificationAsync(json, msg.Tag).Result;
		}
    }
}

namespace GCMMessage
{
    public class RootObject
	{
		public string collapse_key { get; set; }
		public bool delay_while_idle { get; set; }
		public string to { get; set; }
        public string silent { get; set; }
        public Dictionary<string, string> data { get; set; } = new Dictionary<string, string>();
		public int time_to_live { get; set; }
	}
}
namespace APNS
{

    public class Aps
	{
        [JsonProperty("alert")]
		public Dictionary<string, string> Alert { get; set; } = new Dictionary<string, string>();
        [JsonProperty("content-available")]
		public int ContentAvailable { get; set; }
        [JsonProperty("badge")]
		public int Badge { get; set; }
	}

	public class RootObject
	{
        [JsonProperty("aps")]
		public Aps Content { get; set; }
	}
}
