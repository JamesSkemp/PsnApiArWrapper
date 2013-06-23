using System.IO;
using System.Net;
using System.Text;

namespace PsnApiArWrapper
{
	public partial class PsnApi
	{
		private string ApiRequest(string methodPath, string parameters)
		{
			var requestUrl = string.Format(PsnApi.apiUrl, methodPath, parameters);

			var request = WebRequest.CreateHttp(requestUrl);
			request.UserAgent = this.UserAgent;
			request.Timeout = this.Timeout;

			using (var response = request.GetResponse())
			{
				StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
				return reader.ReadToEnd();
			}
		}
	}
}