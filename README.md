# SoundCloud.Api

Full featured SoundCloud API wrapper written in C# using .NET Standard 2.0

## Installation

Install it using NuGet:
```
Install-Package SoundCloud.Api
```
or download the latest archive from the [release page](https://github.com/prayzzz/SoundCloud.Api/releases).

## Usage

### With Dependency Injection

```csharp
serviceCollection.AddSoundCloudClient(string.Empty, "clientId");
```

If you want to customize the used HttpClient use:

```csharp
serviceCollection.AddSingleton<SoundCloudClient>();
var httpClientBuilder = serviceCollection.AddSoundCloudHttpClient(string.Empty, "clientId");
// httpClientBuilder.AddHttpMessageHandler()
// ...
```

### Without Dependency Injection

Use the SoundCloudClient to access the resources provided by SoundCloud:
```csharp
string accessToken = "your access token here";
ISoundCloudClient client = SoundCloudClient.CreateAuthorized(accessToken);

IEnumerable<Tracks> myTracks = client.Me.GetTracks();
```

If you don't want to perfom a login, you can also access the resources by only providing your ClientId. Use this instead:
```csharp
string clientId = "your client id here";
ISoundCloudClient client = SoundCloudClient.CreateUnauthorized(clientId);
```

### How to obtain a ClientId and OAuth Token

As of March 2019 it's not possible to register new apps on soundcloud.com.
Due to that, there's no offical way to obtain a `clientId` or OAuth `token`.

But you can get the `clientId` of your Browser using the DevTools (F12) and investigate the XHR requests.
If you're logged in, you can get an OAuth `token` from your cookie. 

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## License

[MIT License](https://github.com/prayzzz/SoundCloud.Api/blob/master/LICENSE)