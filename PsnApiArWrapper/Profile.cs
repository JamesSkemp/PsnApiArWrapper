namespace PsnApiArWrapper
{
	public partial class PsnApi
	{
		/// <summary>
		/// Get the profile for an individual user.
		/// </summary>
		/// <param name="psnId">Id of the user to query.</param>
		/// <returns>String of data for the user.</returns>
		public string GetProfile(string psnId)
		{
			var parameters = string.Format("sPSNID={0}", psnId);
			return ApiRequest("getPSNID", parameters);
		}
	}
}
