using SoundCloud.Api.Endpoints;

namespace SoundCloud.Api
{
    public interface ISoundCloudClient
    {
        IApps Apps { get; }

        IComments Comments { get; }

        IMe Me { get; }

        IOAuth2 OAuth2 { get; }

        IPlaylists Playlists { get; }

        IResolve Resolve { get; }

        ITracks Tracks { get; }

        IUsers Users { get; }
    }
}