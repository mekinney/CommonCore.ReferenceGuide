using System;
using referenceguide;

namespace Xamarin.Forms.CommonCore
{
    public partial class ObservableViewModel
    {
        public DataBusinessLayer DataBLL
        {
            get
            {
                return InjectionManager.GetBusinessLayer<DataBusinessLayer>();
            }
        }

		public OAuthBusinessLayer OAuthBLL
		{
			get
			{
				return InjectionManager.GetBusinessLayer<OAuthBusinessLayer>();
			}
		}

		public CryptoBusinessLayer CryptoBLL
		{
			get
			{
				return InjectionManager.GetBusinessLayer<CryptoBusinessLayer>();
			}
		}

        public WebBusinessLayer WebBll
        {
			get
			{
				return InjectionManager.GetBusinessLayer<WebBusinessLayer>();
			}
        }
    }
}
