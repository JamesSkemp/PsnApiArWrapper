using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsnApiArWrapper
{
	public partial class PsnApi
	{
		/// <summary>
		/// Get trophies for a particular game for an individual user.
		/// <seealso cref="GetGame"/>
		/// </summary>
		/// <param name="psnId">Id of the user to query.</param>
		/// <param name="gameId">Id of the game to get trophies for.</param>
		/// <returns>String of data for the user.</returns>
		public string GetTrophies(string psnId, string gameId)
		{
			var parameters = string.Format("sPSNID={0}&sGameId={1}", psnId, gameId);
			return ApiRequest("getTrophies", parameters);
		}
	}
}