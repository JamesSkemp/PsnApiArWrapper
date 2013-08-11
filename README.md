PsnApiArWrapper
===============

Wrapper for psnapi.com.ar web services.

Video Games Spa example
===

The following is a C# example that will grab all information for a user for generating a Video Game Spa site.

	// Ready the API.
	var api = new PsnApiArWrapper.PsnApi();
	// Create a request to get information for a particular user.
	var dataRequest = new PsnApiArWrapper.PsnApi.DataRequest() {
		// Id of the user to pull information for.
		PsnId = "strivinglife",
		// Where to output the XML returned by the API.
		OutputDirectory = @"C:\Users\James\Projects\GitHub\VideoGamesSpa\_output\{0}\psnapi\",
		// Format of the actual file; adds .xml to the end.
		FileNameFormat = "{0}",
		// Backup existing files; adds '__' as a prefix and a dash and the current date/time as a prefix.
		BackupFiles = true,
		// Get the user's profile.
		GetProfile = true,
		// Get the listing of all games played by the user.
		GetGames = true,
		// Get information about individual games.
		GetGameTrophyData = true,
		// Get only the last x games.
		GetLastGames = 1
	};
	// Pull the requested data.
	api.PullUserData(dataRequest);


Links
====

Learn about the API: http://www.psnapi.com.ar/

View my implementation: http://media.jamesrskemp.com/vg/Default.html
