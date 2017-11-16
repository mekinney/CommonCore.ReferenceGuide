using System;
using referenceguide;

namespace Xamarin.Forms.CommonCore
{
    public partial class CoreViewModel
    {
        public DataBusinessLayer DataBLL
        {
            get
            {
                return CoreDependencyService.GetBusinessLayer<DataBusinessLayer>();
            }
        }

		public OAuthBusinessLayer OAuthBLL
		{
			get
			{
				return CoreDependencyService.GetBusinessLayer<OAuthBusinessLayer>();
			}
		}

		public CryptoBusinessLayer CryptoBLL
		{
			get
			{
				return CoreDependencyService.GetBusinessLayer<CryptoBusinessLayer>();
			}
		}

        public WebBusinessLayer WebBll
        {
			get
			{
				return CoreDependencyService.GetBusinessLayer<WebBusinessLayer>();
			}
        }
    }
}
