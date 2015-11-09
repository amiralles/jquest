
using System;
using System.Threading;
using static System.Console;
using static JQuest;

class JQuestTester {
	static int Main(string[] argv) {
		bool doneposting = false, donegetting = false;

		try {
			var url = "http://jsonplaceholder.typicode.com/posts";
			var obj = new { title = "tests", body = "foo", author = "amiralles" };

			// ==================================================================
			// POST
			// ==================================================================
			WriteLine("POST (sync)");
			// Sync API
			var res = PostJsonSync(url, obj);
			WriteLine(res);


			// Acync API
			Action<string> 
				onSuccess = data => { WriteLine(data); doneposting = true; },
				onError   = err  => { WriteLine(err);  doneposting = true; };

			WriteLine("POST (async)");
			PostJsonAsync(url, obj, onSuccess, onError);


			// ==================================================================
			// GET
			// ==================================================================
			// Sync API
			WriteLine("GET (sync)");
			url = "http://jsonplaceholder.typicode.com/posts/1";

			var getRes = GetJsonSync(url);
			WriteLine(getRes);

			// Acync API
			Action<string> 
				onGetSuccess = data => { WriteLine(data); donegetting = true; },
				onGetError   = err  => { WriteLine(err) ; donegetting = true; };

			WriteLine("GET (async)");
			GetJsonAsync(url, onGetSuccess, onGetError);
			// ==================================================================



		}
		catch (Exception ex) {
			WriteLine(ex.Message);
		}

		while(!doneposting && !donegetting)
			Thread.Sleep(1);

		Thread.Sleep(1000);
		WriteLine("Press [enter] to exit");
		ReadLine();
		return 0;
	}
}
