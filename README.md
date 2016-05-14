# SoundCloud.Api
Full C# API wrapper for SoundCloud

 Config        | Status
---------------|---
CI             | [![Build status](https://ci.appveyor.com/api/projects/status/32lb7fq7dstdelt9?svg=true)](https://ci.appveyor.com/project/prayzzz/soundcloud-api)
Release (v0.8) | [![Build status](https://ci.appveyor.com/api/projects/status/8u3670f0fg38i4nw?svg=true)](https://ci.appveyor.com/project/prayzzz/soundcloud-api-sow3r)

## Installation

Install it using NuGet:
```
Install-Package SoundCloud.Api
```
or download the latest archive from the [release page](https://github.com/prayzzz/SoundCloud.Api/releases).

## Usage

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

Please read the [wiki](https://github.com/prayzzz/SoundCloud.Api/wiki) for additional informations.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## License

[MIT License](https://github.com/prayzzz/SoundCloud.Api/blob/master/LICENSE)