using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms.CommonCore;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreSettings
	{
        public const string FastRenderers = "FastRenderers_Experimental";
        public const string RefreshAppoints = "RefreshAppoints";
        public const string PhoneCallBack = "PhoneCallBack";

		public static readonly long DefaultUTCTicks = new DateTime(2000, 1, 1).Ticks;

		public static long LastSync
		{
			get { return _appSettings.GetValueOrDefault("LastSync", CoreSettings.DefaultUTCTicks); }
			set { _appSettings.AddOrUpdateValue("LastSync", value); }
		}

	}
}
