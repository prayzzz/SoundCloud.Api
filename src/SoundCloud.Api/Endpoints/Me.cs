using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Me : Endpoint, IMe
    {
        private const string MeActivitiesPath = "me/activities?";
        private const string MeCommentsPath = "me/comments?";
        private const string MeConnectionsPath = "me/connections?";
        private const string MeFavoritesPath = "me/favorites?";
        private const string MeFavoritePath = "me/favorites/{0}?";
        private const string MeFollowersPath = "me/followers?";
        private const string MeFollowingsPath = "me/followings?";
        private const string MeFollowPath = "me/followings/{0}?";
        private const string MePath = "me?";
        private const string MePlaylistsPath = "me/playlists?";
        private const string MeTracksPath = "me/tracks?";
        private const string MeWebProfilePath = "me/web-profiles/{0}?";
        private const string MeWebProfilesPath = "me/web-profiles?";

        public Me(ISoundCloudApiGateway gateway) : base(gateway)
        {
        }

        public async Task<StatusResponse> DeleteWebProfileAsync(WebProfile profile)
        {
            profile.ValidateDelete();

            var builder = new MeQueryBuilder { Path = string.Format(MeWebProfilePath, profile.Id) };
            return await Gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<StatusResponse> FollowAsync(User user)
        {
            user.ValidateFollowUnfollow();

            var builder = new MeQueryBuilder { Path = string.Format(MeFollowPath, user.Id) };
            return await Gateway.SendPutRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<User> GetAsync()
        {
            var builder = new MeQueryBuilder { Path = MePath };
            return await Gateway.SendGetRequestAsync<User>(builder.BuildUri());
        }

        public Task<SoundCloudList<Activity>> GetActivitiesAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeActivitiesPath, Paged = true, Limit = limit };
            return GetPage<Activity>(builder.BuildUri());
        }

        public Task<SoundCloudList<Comment>> GetCommentsAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeCommentsPath, Paged = true, Limit = limit };
            return GetPage<Comment>(builder.BuildUri());
        }

        public Task<SoundCloudList<Connection>> GetConnectionsAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeConnectionsPath, Paged = true, Limit = limit };
            return GetPage<Connection>(builder.BuildUri());
        }

        public Task<SoundCloudList<Track>> GetFavoritesAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeFavoritesPath, Paged = true, Limit = limit };
            return GetPage<Track>(builder.BuildUri());
        }

        public Task<SoundCloudList<User>> GetFollowersAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeFollowersPath, Paged = true, Limit = limit };
            return GetPage<User>(builder.BuildUri());
        }

        public Task<SoundCloudList<User>> GetFollowingsAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeFollowingsPath, Paged = true, Limit = limit };
            return GetPage<User>(builder.BuildUri());
        }

        public Task<SoundCloudList<Playlist>> GetPlaylistsAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MePlaylistsPath, Paged = true, Limit = limit };
            return GetPage<Playlist>(builder.BuildUri());
        }

        public Task<SoundCloudList<Track>> GetTracksAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeTracksPath, Paged = true, Limit = limit };
            return GetPage<Track>(builder.BuildUri());
        }

        public Task<SoundCloudList<WebProfile>> GetWebProfilesAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new MeQueryBuilder { Path = MeWebProfilesPath, Paged = true, Limit = limit };
            return GetPage<WebProfile>(builder.BuildUri());
        }

        public async Task<StatusResponse> LikeAsync(Track track)
        {
            track.ValidateLikeUnlike();

            var builder = new MeQueryBuilder { Path = string.Format(MeFavoritePath, track.Id) };
            return await Gateway.SendPutRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<WebProfile> PostWebProfileAsync(WebProfile profile)
        {
            profile.ValidatePost();

            var builder = new MeQueryBuilder { Path = MeWebProfilesPath };
            return await Gateway.SendPostRequestAsync<WebProfile>(builder.BuildUri(), profile);
        }

        public async Task<StatusResponse> UnfollowAsync(User user)
        {
            user.ValidateFollowUnfollow();

            var builder = new MeQueryBuilder { Path = string.Format(MeFollowPath, user.Id) };
            return await Gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<StatusResponse> UnlikeAsync(Track track)
        {
            track.ValidateLikeUnlike();

            var builder = new MeQueryBuilder { Path = string.Format(MeFavoritePath, track.Id) };
            return await Gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }
    }
}
