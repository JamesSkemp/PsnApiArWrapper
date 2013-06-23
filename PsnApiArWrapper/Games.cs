namespace PsnApiArWrapper
{
	public partial class PsnApi
	{
		/// <summary>
		/// Get an individual game for an individual user.
		/// <seealso cref="GetTrophies"/>
		/// </summary>
		/// <param name="psnId">Id of the user to query.</param>
		/// <param name="gameId">Id of the game to query.</param>
		/// <returns>String of data for the user and game.</returns>
		public string GetGame(string psnId, string gameId)
		{
			var parameters = string.Format("sPSNID={0}&sIdGame={1}", psnId, gameId);
			return ApiRequest("getGame", parameters);
		}

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