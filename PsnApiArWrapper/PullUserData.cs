using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PsnApiArWrapper
{
	public partial class PsnApi
	{
		/// <summary>
		/// Pulls a collection of data for a particular user.
		/// </summary>
		/// <returns>True if the request is successful.</returns>
		public bool PullUserData(DataRequest dataRequest)
		{
			var pauseDelay = 3000;

			if (string.IsNullOrWhiteSpace(dataRequest.OutputDirectory))
			{
				throw new InvalidOperationException("You must provide a path to the directory to output data to.");
			}
			else if (string.IsNullOrWhiteSpace(dataRequest.FileNameFormat))
			{
				throw new InvalidOperationException("You must provide a file name format.");
			}
			else if (string.IsNullOrWhiteSpace(dataRequest.PsnId))
			{
				throw new InvalidOperationException("You must provide the PlayStation Network id of the user to get data for.");
			}

			var outputDirectory = string.Format(dataRequest.OutputDirectory, dataRequest.PsnId);
			var filePath = Path.Combine(outputDirectory, dataRequest.FileNameFormat + ".xml");
			if (!Directory.Exists(outputDirectory))
			{
				Directory.CreateDirectory(outputDirectory);
			}

			var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");

			if (dataRequest.GetProfile)
			{
				var profilePath = string.Format(filePath, "profile");
				if (dataRequest.BackupFiles && File.Exists(profilePath))
				{
					File.Copy(profilePath, "__" + profilePath + "." + currentTime + ".xml");
				}
				XDocument.Parse(this.GetProfile(dataRequest.PsnId)).Save(profilePath);
				Thread.Sleep(pauseDelay);
			}
			var gamesPath = string.Format(filePath, "games");
			if (dataRequest.GetGames)
			{
				if (dataRequest.BackupFiles && File.Exists(gamesPath))
				{
					File.Copy(gamesPath, "__" + gamesPath + "." + currentTime + ".xml");
				}
				XDocument.Parse(this.GetGames(dataRequest.PsnId)).Save(gamesPath);
				Thread.Sleep(pauseDelay);
			}
			if (dataRequest.GetGameTrophyData)
			{
				if (!File.Exists(gamesPath))
				{
					throw new NotImplementedException("Games data does not exist. Please GetGames first.");
				}
				if (dataRequest.GameIds != null && dataRequest.GameIds.Count > 0)
				{
					// Requesting very specific games.
					foreach (var gameId in dataRequest.GameIds)
					{
						var gamePath = string.Format(filePath, "game-" + gameId);
						if (dataRequest.BackupFiles && File.Exists(gamePath))
						{
							File.Copy(gamePath, "__" + gamePath + "." + currentTime + ".xml");
						}
						XDocument.Parse(this.GetGame(dataRequest.PsnId, gameId)).Save(gamePath);
						Thread.Sleep(pauseDelay);
					}
				}
				else
				{
					// We need to get what games the user has played and then get data for them.
					var playedGames = XDocument.Load(gamesPath).Root.Descendants(apiNs + "Game");
					if (dataRequest.GetLastGames.HasValue && dataRequest.GetLastGames.Value > 0)
					{
						// Limit to only the last x games.
						playedGames = playedGames.Take(dataRequest.GetLastGames.Value);
					}
					foreach (var playedGame in playedGames)
					{
						var gameId = playedGame.Element(apiNs + "Id").Value;
						var gamePath = string.Format(filePath, "trophies-" + gameId);
						if (dataRequest.BackupFiles && File.Exists(gamePath))
						{
							File.Copy(gamePath, "__" + gamePath + "." + currentTime + ".xml");
						}
						XDocument.Parse(this.GetTrophies(dataRequest.PsnId, gameId)).Save(gamePath);
						Thread.Sleep(pauseDelay);
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Data request options for pulling a user's data.
		/// </summary>
		public class DataRequest
		{
			/// <summary>
			/// PlayStation Network id of the user to get data for.
			/// </summary>
			public string PsnId { get; set; }
			/// <summary>
			/// Whether to get profile data for the user.
			/// </summary>
			public bool GetProfile { get; set; }
			/// <summary>
			/// Whether to get a listing of the user's games.
			/// </summary>
			public bool GetGames { get; set; }
			/// <summary>
			/// Whether to get individual game data for the user.
			/// </summary>
			public bool GetGameTrophyData { get; set; }
			/// <summary>
			/// List of game ids to pull for the user. All if empty.
			/// </summary>
			public List<string> GameIds { get; set; }
			/// <summary>
			/// Get the last x games if defined, otherwise pulls all.
			/// </summary>
			public int? GetLastGames { get; set; }
			/*/// <summary>
			/// Whether to get the list of friends for the user.
			/// </summary>
			public bool GetFriends { get; set; }*/
			/// <summary>
			/// Directory to output data to. Will replace {0} with the user's id, if provided.
			/// </summary>
			public string OutputDirectory { get; set; }
			/// <summary>
			/// File name format to use for the files generated. Defaults to psnapiar-{0}.
			/// </summary>
			public string FileNameFormat { get; set; }
			/// <summary>
			/// Whether to backup any existing files before replacing them.
			/// </summary>
			public bool BackupFiles { get; set; }

			public DataRequest()
			{
				this.FileNameFormat = "psnapiar-{0}";
			}

			/// <summary>
			/// Pull all profile/game/friend data for a user, but only get updated information for a certain number of games.
			/// </summary>
			/// <param name="gamesToPull">Number of recent games to pull.</param>
			public DataRequest(int gamesToPull)
				: this()
			{
				this.GetProfile = true;
				this.GetGames = true;
				//this.GetFriends = true;
				this.GetLastGames = gamesToPull;
				this.BackupFiles = true;
			}
		}
	}
}
