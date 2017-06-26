using System;
namespace referenceguide.Models
{
	public class PushMessage
	{
		public int DeviceType { get; set; }
		public string Message { get; set; }
		public string Tag { get; set; }
		public bool Success { get; set; }
	}
}
