using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.CommonCore;

namespace referenceguide
{
    public class WebBusinessLayer:CoreBusiness
    {
		private WebDownloadClient downloadClient;

		public async Task<(List<RandomUser> Response, Exception Error)> GetRandomUsers()
		{
			var url = this.WebApis["randomuser"];
			var result = await this.HttpService.Get<RootObject>(url);
            result.Error?.LogException("WebBusinessLayer - GetRandomUsers");
			if (result.Error==null)
			{
                var lst = result.Response.results.ToRandomUserList();
                return (lst, null);
			}
			else
			{
				return (null, result.Error);
			}
		}

		public async Task<(List<RandomUser> Response, Exception Error)> GetPaginatedRandomUsers(int pageIndex)
		{
			var url = this.WebApis["randomuserpaginated"];
			url = string.Format(url, pageIndex);
			var result = await this.HttpService.Get<RootObject>(url);
			result.Error?.LogException("WebBusinessLayer - GetPaginatedRandomUsers");
			if (result.Error == null)
			{
				var lst = result.Response.results.ToRandomUserList();
				return (lst, null);
			}
			else
			{
				return (null, result.Error);
			}
		}

        public async Task<(PostItem Response, bool Success, Exception Error)> PostDataExample()
        {
            try
            {
				var url = this.WebApis["referencewebtestpost"];
				var p = new PostItem() { FirstName = "Jack", LastName = "Sparrow", Age = 21 };
				var result = await HttpService.Post<PostItem>(url, p);
                result.Error?.LogException("WebBusinessLayer - PostDataExample");
                return result;
            }
            catch (Exception ex)
            {
                ex?.LogException("WebBusinessLayer - PostDataExample");
                return (null, false, ex);
            }
        }

        public async Task GetLongDownload(Action<double> percentCallBack, Action<object> completed)
        {
			downloadClient = HttpService.GetWebDownloadClient();
			downloadClient.DownloadUrl = this.WebApis["largefile"];
			downloadClient.PercentageChanged += (percent) => {
                percentCallBack?.Invoke(percent);
			};
			downloadClient.FinishedEvent += (data) => {
                completed?.Invoke(data);
			};
			await downloadClient.StartDownload();
        }
    }
}
