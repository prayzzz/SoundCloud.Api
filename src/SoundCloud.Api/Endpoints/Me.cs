using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Utils;

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
        private const string MeGroupsPath = "me/groups?";
        private const string MePath = "me?";
        private const string MePlaylistsPath = "me/playlists?";
        private const string MeTracksPath = "me/tracks?";
        private const string MeWebProfilePath = "me/web-profiles/{0}?";
        private const string MeWebProfilesPath = "me/web-profiles?";

        public Me(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<IWebResult> DeleteWebProfileAsync(WebProfile profile)
        {
            Validate(profile.ValidateDelete);

            var builder = new MeQueryBuilder { Path = string.Format(MeWebProfilePath, profile.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<IWebResult> FollowAsync(User user)
        {
            Validate(user.ValidateFollowUnfollow);

            var builder = new MeQueryBuilder { Path = string.Format(MeFollowPath, user.Id) };
            return await UpdateAsync(builder.BuildUri());
        }

        public async Task<User> GetAsync()
        {
            var builder = new MeQueryBuilder { Path = MePath };
            return await GetByIdAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            var builder = new MeQueryBuilder { Path = MeActivitiesPath, Paged = true };
            return await GetListAsync<Activity>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeCommentsPath, Paged = true };
            return await GetListAsync<Comment>(builder.BuildUri());
        }

        public async Task<IEnumerable<Connection>> GetConnectionsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeConnectionsPath, Paged = true };
            return await GetListAsync<Connection>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetFavoritesAsync()
        {
            var builder = new MeQueryBuilder { Path = MeFavoritesPath, Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetFollowersAsync()
        {
            var builder = new MeQueryBuilder { Path = MeFollowersPath, Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetFollowingsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeFollowingsPath, Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            var builder = new MeQueryBuilder { Path = MeGroupsPath, Paged = true };
            return await GetListAsync<Group>(builder.BuildUri());
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
        {
            var builder = new MeQueryBuilder { Path = MePlaylistsPath, Paged = true };
            return await GetListAsync<Playlist>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetTracksAsync()
        {
            var builder = new MeQueryBuilder { Path = MeTracksPath, Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<WebProfile>> GetWebProfilesAsync()
        {
            var builder = new MeQueryBuilder { Path = MeWebProfilesPath, Paged = true };
            return await GetListAsync<WebProfile>(builder.BuildUri());
        }

        public async Task<IWebResult> LikeAsync(Track track)
        {
            Validate(track.ValidateLikeUnlike);

            var builder = new MeQueryBuilder { Path = string.Format(MeFavoritePath, track.Id) };
            return await UpdateAsync(builder.BuildUri());
        }

        public async Task<IWebResult<WebProfile>> PostWebProfileAsync(WebProfile profile)
        {
            Validate(profile.ValidatePost);

            var builder = new MeQueryBuilder { Path = MeWebProfilesPath };
            return await CreateAsync<WebProfile>(builder.BuildUri(), profile);
        }

        public async Task<IWebResult> UnfollowAsync(User user)
        {
            Validate(user.ValidateFollowUnfollow);

            var builder = new MeQueryBuilder { Path = string.Format(MeFollowPath, user.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<IWebResult> UnlikeAsync(Track track)
        {
            Validate(track.ValidateLikeUnlike);

            var builder = new MeQueryBuilder { Path = string.Format(MeFavoritePath, track.Id) };
            return await DeleteAsync(builder.BuildUri());
        }
    }
}