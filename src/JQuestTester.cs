
using System;
using static System.Console;
using static JQuest;

class JQuestTester {
	static int Main(string[] argv) {
		try {
			var url = "http://jsonplaceholder.typicode.com/posts";
			var obj = new { title = "tests", body = "foo", author = "amiralles" };

			//Sync API
			var res = PostJsonSync(url, obj);
			WriteLine(res);


			//Acync API
			Action<string> 
				onSuccess = data => WriteLine(data),
				onError   = err  => WriteLine(err);

			PostJsonAsync(url, obj, onSuccess, onError);
			WriteLine("Waiting...");
		}
		catch (Exception ex) {
			WriteLine(ex.Message);
		}

		WriteLine("Press [enter] to exit");
		ReadLine();
		return 0;
	}
}
