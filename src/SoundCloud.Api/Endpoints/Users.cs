using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Endpoints
{
    internal class Users : Endpoint, IUsers
    {
        private const string UserCommentsPath = "users/{0}/comments?";
        private const string UserFavoritesPath = "users/{0}/favorites?";
        private const string UserFollowersPath = "users/{0}/followers?";
        private const string UserFollowingsPath = "users/{0}/followings?";
        private const string UserGroupsPath = "users/{0}/groups?";
        private const string UserPath = "users/{0}?";
        private const string UserPlaylistsPath = "users/{0}/playlists?";
        private const string UsersPath = "users?";
        private const string UserTracksPath = "users/{0}/tracks?";
        private const string UserWebProfilesPath = "users/{0}/web-profiles?";

        public Users(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await GetAsync(new UserQueryBuilder());
        }

        public async Task<IEnumerable<User>> GetAsync(UserQueryBuilder queryBuilder)
        {
            queryBuilder.Path = UsersPath;
            queryBuilder.Paged = true;

            return await GetListAsync<User>(queryBuilder.BuildUri());
        }

        public async Task<User> GetAsync(int userId)
        {
            var builder = new UserQueryBuilder { Path = string.Format(UserPath, userId) };
            return await GetByIdAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserCommentsPath, user.Id), Paged = true };
            return await GetListAsync<Comment>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetFavoritesAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserFavoritesPath, user.Id), Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserFollowersPath, user.Id), Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetFollowingsAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserFollowingsPath, user.Id), Paged = true };
            return await GetListAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserGroupsPath, user.Id), Paged = true };
            return await GetListAsync<Group>(builder.BuildUri());
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserPlaylistsPath, user.Id), Paged = true };
            return await GetListAsync<Playlist>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserTracksPath, user.Id), Paged = true };
            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<WebProfile>> GetWebProfilesAsync(User user)
        {
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder { Path = string.Format(UserWebProfilesPath, user.Id), Paged = true };
            return await GetListAsync<WebProfile>(builder.BuildUri());
        }
    }
}