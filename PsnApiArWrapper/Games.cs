namespace PsnApiArWrapper
{
	public partial class PsnApi{
		public string GetGames(string psnId)
		{
			var parameters = string.Format("sPSNID={0}", psnId);
			return ApiRequest("getGames", parameters);
		}
	}
}