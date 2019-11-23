# SoundCloud.Api

[![Nuget](https://img.shields.io/nuget/v/SoundCloud.Api.svg?style=flat-square)](https://www.nuget.org/packages/SoundCloud.Api/)

Full featured SoundCloud API wrapper written in C# using .NET Standard 2.0

## Installation

Install it using [NuGet](https://www.nuget.org/packages/SoundCloud.Api/):
```
Install-Package SoundCloud.Api
```

## Information

SoundCloud.Api uses `IHttpClientFactory` to make Http requests under the hood.
* see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests

## Usage

### With Dependency Injection

```csharp
serviceCollection.AddSoundCloudClient(string.Empty, "clientId");
```

If you want to customize the used HttpClient:

```csharp
var httpClientBuilder = serviceCollection.AddSingleton<SoundCloudClient>()
                                         .AddSoundCloudHttpClient(string.Empty, "clientId");
                                         
// httpClientBuilder.AddHttpMessageHandler(...)
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

If you're in possession of a `ClientId` and `ClientSecret`, you can use `SoundCloudOAuth` to obtain a `AccessToken` before creating the `SoundCloudClient`.
Refreshing the `AccessToken` has to be done by you. Use the OAuth Endpoint and replace the `AccessToken` in `yourClientInstance.AuthInfo.AccessToken`.


### Lists

SoundClouds search / list APIs use `linked_partitioning`.
Every response contains a URI to the next page.
`SoundCloudList` maps this behavior:

```csharp
var tracks = await client.Tracks.GetAllAsync();

while(true)
{
    foreach (var track in tracks)
    {
        Console.WriteLine(track.Title);
    }
    
    if (tracks.HasNextPage)
    {
        tracks = await tracks.GetNextPageAsync();
    }
    else
    {
        break;
    }
}
```

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## License

[MIT License](https://github.com/prayzzz/SoundCloud.Api/blob/master/LICENSE)
