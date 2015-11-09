
# JQuest
JQuest is a small library that will help you to test REST APIs from .NET applications. Its main goal is to reduce the boilerplate code you have to write to POST or GET json from HTTP endpoints.

**So, instead of writing this:**

```cs
		var url = "http://jsonplaceholder.typicode.com/posts";
		var obj = new { title = "tests", body = "foo", author = "amiralles" };
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
```

**You can write this, and go out for coffee ;)**

```cs
	var url = "http://jsonplaceholder.typicode.com/posts";
	var obj = new { title = "tests", body = "foo", author = "amiralles" };
	var res = PostJsonSync(url, obj);
```

**And of course, it also have an async version:**
```cs
	var url = "http://jsonplaceholder.typicode.com/posts";
	var obj = new { title = "tests", body = "foo", author = "amiralles" };
	Action<string> 
		onSuccess = data => WriteLine(data),
		onError   = err  => WriteLine(err);

	PostJsonAsync(url, obj, onSuccess, onError);

```



**And this is how you send GET requests**

```cs
	var url = "http://jsonplaceholder.typicode.com/posts/1";

	// Sync API
	var getRes = GetJsonSync(url);
	WriteLine(getRes);

	// Acync API
	Action<string> 
		onGetSuccess = data => WriteLine(data),
		onGetError   = err  => WriteLine(err);

	GetJsonAsync(url, onGetSuccess, onGetError);

```

## How to build
If you are running on mac or linux just execute build.sh to build the library or build_tester.sh to build the tesing console app. Otherwise you can use csc.exe from the Windows Command Line.





 
