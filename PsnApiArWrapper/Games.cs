﻿namespace PsnApiArWrapper
{
	public partial class PsnApi
	{
		/// <summary>
		/// Get games for an individual user.
		/// </summary>
		/// <param name="psnId">Id of the user to query.</param>
		/// <returns>String of data for the user.</returns>
		public string GetGames(string psnId)
		{
			var parameters = string.Format("sPSNID={0}", psnId);
			return ApiRequest("getGames", parameters);
		}
	}
}