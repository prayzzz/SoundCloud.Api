using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Me : IMe
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

        private readonly ISoundCloudApiGateway _gateway;

        public Me(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<StatusResponse> DeleteWebProfileAsync(WebProfile profile)
        {
            profile.ValidateDelete();

            var builder = new MeQueryBuilder { Path = string.Format(MeWebProfilePath, profile.Id) };
            return await _gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<StatusResponse> FollowAsync(User user)
        {
            user.ValidateFollowUnfollow();

            var builder = new MeQueryBuilder { Path = string.Format(MeFollowPath, user.Id) };
            return await _gateway.SendPutRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<User> GetAsync()
        {
            var builder = new MeQueryBuilder { Path = MePath };
            return await _gateway.SendGetRequestAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            var builder = new MeQueryBuilder { Path = MeActivitiesPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Activity>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeCommentsPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Comment>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Connection>> GetConnectionsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeConnectionsPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Connection>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Track>> GetFavoritesAsync()
        {
            var builder = new MeQueryBuilder { Path = MeFavoritesPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Track>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<User>> GetFollowersAsync()
        {
            var builder = new MeQueryBuilder { Path = MeFollowersPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<User>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<User>> GetFollowingsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeFollowingsPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<User>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
        {
            var builder = new MeQueryBuilder { Path = MePlaylistsPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Playlist>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Track>> GetTracksAsync()
        {
            var builder = new MeQueryBuilder { Path = MeTracksPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Track>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<WebProfile>> GetWebProfilesAsync()
        {
            var builder = new MeQueryBuilder { Path = MeWebProfilesPath, Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<WebProfile>>(builder.BuildUri())).Collection;
        }

        public async Task<StatusResponse> LikeAsync(Track track)
        {
            track.ValidateLikeUnlike();

            var builder = new MeQueryBuilder { Path = string.Format(MeFavoritePath, track.Id) };
            return await _gateway.SendPutRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<WebProfile> PostWebProfileAsync(WebProfile profile)
        {
            profile.ValidatePost();

            var builder = new MeQueryBuilder { Path = MeWebProfilesPath };
            return await _gateway.SendPostRequestAsync<WebProfile>(builder.BuildUri(), profile);
        }

        public async Task<StatusResponse> UnfollowAsync(User user)
        {
            user.ValidateFollowUnfollow();

            var builder = new MeQueryBuilder { Path = string.Format(MeFollowPath, user.Id) };
            return await _gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<StatusResponse> UnlikeAsync(Track track)
        {
            track.ValidateLikeUnlike();

            var builder = new MeQueryBuilder { Path = string.Format(MeFavoritePath, track.Id) };
            return await _gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }
    }
}