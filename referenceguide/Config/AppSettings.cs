using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class AppSettings: CoreSettings
	{
		public const string RefreshAppoints = "RefreshAppoints";

		public static readonly long DefaultUTCTicks = new DateTime(2000, 1, 1).Ticks;
		private static ISettings _appSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		public static long LastSync
		{
			get { return _appSettings.GetValueOrDefault("LastSync", AppSettings.DefaultUTCTicks); }
			set { _appSettings.AddOrUpdateValue("LastSync", value); }
		}

	}
}
