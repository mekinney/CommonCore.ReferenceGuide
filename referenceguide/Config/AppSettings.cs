using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace referenceguide
{
	public class AppSettings
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
		public static string AESEncryptionKey
		{
			get { return _appSettings.GetValueOrDefault<string>("AESEncryptionKey", null); }
			set { _appSettings.AddOrUpdateValue<string>("AESEncryptionKey", value); }
		}

		public static long LastSync
		{
			get { return _appSettings.GetValueOrDefault<long>("LastSync", AppSettings.DefaultUTCTicks); }
			set { _appSettings.AddOrUpdateValue<long>("LastSync", value); }
		}

		public static string InstallationId
		{
			get { return _appSettings.GetValueOrDefault<string>("InstallationId", null); }
			set { _appSettings.AddOrUpdateValue<string>("InstallationId", value); }
		}

	}
}
