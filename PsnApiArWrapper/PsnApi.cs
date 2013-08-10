using System.Xml.Linq;
namespace PsnApiArWrapper
{
	/// <summary>
	/// Functionality to connect to the psnapi.com.ar API.
	/// </summary>
	public partial class PsnApi
	{
		#region
		/// <summary>
		/// Base URL of the API, ready for String.Format (passing method then query parameters).
		/// </summary>
		private const string apiUrl = "http://www.psnapi.com.ar/ps3/api/psn.asmx/{0}?{1}";
		/// <summary>
		/// Namespace of the API, for use when parsing XML returned by it.
		/// </summary>
		private XNamespace apiNs = "http://www.psnapi.com.ar/ps3/api";
		#endregion
		/// <summary>
		/// Timeout (in milliseconds) to use when making a request.
		/// </summary>
		public int Timeout { get; set; }
		/// <summary>
		/// Browser user-agent to use when requesting data.
		/// </summary>
		public string UserAgent { get; set; }

		/// <summary>
		/// Initialize a new instances of the API.
		/// </summary>
		public PsnApi()
		{
			this.Timeout = 15000;
			this.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.116 Safari/537.36";
		}
	}
}