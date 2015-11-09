using System;
using System.Threading;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using static System.Console;

public class JQuest {

	/// Sends a synchronous POST request to the specified URL.
	public static string PostJsonSync(string url, object obj) {
		DieIf(string.IsNullOrEmpty(url), $"nameof(url) cannot be null or empty.");
		DieIf(obj == null, $"nameof(obj) cannot be null.");
		
		var httpWebRequest         = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.ContentType = "application/json";
		httpWebRequest.Method      = "POST";

		using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
			var json = JsonConvert.SerializeObject(obj);
			streamWriter.Write(json);
			streamWriter.Flush();
			streamWriter.Close();
		}

		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
			var result = streamReader.ReadToEnd();

			return result;
		}
	}

	/// Sends an asynchronous POST request to the specified URL.
	public static void PostJsonAsync(string url, object obj, Action<string> onSuccess, Action<string> onError) {
		DieIf(string.IsNullOrEmpty(url), $"nameof(url) cannot be null or empty.");
		DieIf(obj == null, $"nameof(obj) cannot be null.");

		var t = new Thread(()=> {
			try {
				var res = PostJsonSync(url, obj);

				if (onSuccess != null)
					onSuccess(res);
			}
			catch (Exception ex) {
				if (onError != null) onError(ex.Message);
				else throw;
			}
		});

		t.Start();
	}


	/*
	/// Sends an asynchronous POST request to the specified URL.
	public static void GetJsonAsync(string url, object args, Func<string> onSuccess, Func<string> onError) {
		DieIf(string.IsNullOrEmpty(url), $"nameof(url) cannot be null or empty.");

		var t = new Thread(()=> {
			try {
				var res = GetJsonSync(url, args);

				if (onSuccess != null)
					onSuccess(res);
			}
			catch (Exception ex) {
				if (onError != null) onError(ex.Message);
				else throw;
			}
		});

		t.Start();
	}
*/

	static void DieIf(bool cnd, string msg) {
		if (cnd)
			throw new JQuestException (msg);
	}
}

class JQuestException : Exception {
	public JQuestException(string msg)  : base (msg) {}
}
