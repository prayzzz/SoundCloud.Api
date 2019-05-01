using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Users : Endpoint, IUsers
    {
        private const string UserCommentsPath = "users/{0}/comments?";
        private const string UserFavoritesPath = "users/{0}/favorites?";
        private const string UserFollowersPath = "users/{0}/followers?";
        private const string UserFollowingsPath = "users/{0}/followings?";
        private const string UserPath = "users/{0}?";
        private const string UserPlaylistsPath = "users/{0}/playlists?";
        private const string UsersPath = "users?";
        private const string UserTracksPath = "users/{0}/tracks?";
        private const string UserWebProfilesPath = "users/{0}/web-profiles?";

        public Users(ISoundCloudApiGateway gateway) : base(gateway)
        {
        }

        public async Task<SoundCloudList<User>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            return await GetAllAsync(new UserQueryBuilder { Limit = limit });
        }

        public Task<SoundCloudList<User>> GetAllAsync(UserQueryBuilder builder)
        {
            builder.Path = UsersPath;
            builder.Paged = true;

            return GetPage<User>(builder.BuildUri());
        }

        public async Task<User> GetAsync(long userId)
        {
            var builder = new UserQueryBuilder { Path = string.Format(UserPath, userId) };
            return await Gateway.SendGetRequestAsync<User>(builder.BuildUri());
        }

        public Task<SoundCloudList<Comment>> GetCommentsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserCommentsPath, user.Id), Paged = true, Limit = limit };
            return GetPage<Comment>(builder.BuildUri());
        }

        public Task<SoundCloudList<Track>> GetFavoritesAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserFavoritesPath, user.Id), Paged = true, Limit = limit };
            return GetPage<Track>(builder.BuildUri());
        }

        public Task<SoundCloudList<User>> GetFollowersAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserFollowersPath, user.Id), Paged = true, Limit = limit };
            return GetPage<User>(builder.BuildUri());
        }

        public Task<SoundCloudList<User>> GetFollowingsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserFollowingsPath, user.Id), Paged = true, Limit = limit };
            return GetPage<User>(builder.BuildUri());
        }

        public Task<SoundCloudList<Playlist>> GetPlaylistsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserPlaylistsPath, user.Id), Paged = true, Limit = limit };
            return GetPage<Playlist>(builder.BuildUri());
        }

        public Task<SoundCloudList<Track>> GetTracksAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserTracksPath, user.Id), Paged = true, Limit = limit };
            return GetPage<Track>(builder.BuildUri());
        }

        public Task<SoundCloudList<WebProfile>> GetWebProfilesAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserWebProfilesPath, user.Id), Paged = true, Limit = limit };
            return GetPage<WebProfile>(builder.BuildUri());
        }
    }
}
