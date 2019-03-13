using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public IEnumerable<User> Get()
        {
            EnsureClientId();

            return Get(new UserQueryBuilder());
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            EnsureClientId();

            return await GetAsync(new UserQueryBuilder());
        }

        public IEnumerable<User> Get(UserQueryBuilder queryBuilder)
        {
            EnsureClientId();

            queryBuilder.Path = UsersPath;
            queryBuilder.Paged = true;

            return GetList<User>(queryBuilder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetAsync(UserQueryBuilder queryBuilder)
        {
            EnsureClientId();

            queryBuilder.Path = UsersPath;
            queryBuilder.Paged = true;

            return await GetListAsync<User>(queryBuilder.BuildUri());
        }

        public User Get(int userId)
        {
            EnsureClientId();

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserPath, userId);

            return GetById<User>(builder.BuildUri());
        }

        public async Task<User> GetAsync(int userId)
        {
            EnsureClientId();

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserPath, userId);

            return await GetByIdAsync<User>(builder.BuildUri());
        }
        
        public IEnumerable<Comment> GetComments(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserCommentsPath, user.id);
            builder.Paged = true;

            return GetList<Comment>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserCommentsPath, user.id);
            builder.Paged = true;

            return await GetListAsync<Comment>(builder.BuildUri());
        }

        public IEnumerable<Track> GetFavorites(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserFavoritesPath, user.id);
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }
        
        public async Task<IEnumerable<Track>> GetFavoritesAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserFavoritesPath, user.id);
            builder.Paged = true;

            return await GetListAsync<Track>(builder.BuildUri());
        }

        public IEnumerable<User> GetFollowers(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserFollowersPath, user.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserFollowersPath, user.id);
            builder.Paged = true;

            return await GetListAsync<User>(builder.BuildUri());
        }

        public IEnumerable<User> GetFollowings(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserFollowingsPath, user.id);
            builder.Paged = true;

            return GetList<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<User>> GetFollowingsAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserFollowingsPath, user.id);
            builder.Paged = true;

            return await GetListAsync<User>(builder.BuildUri());
        }

        public IEnumerable<Group> GetGroups(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserGroupsPath, user.id);
            builder.Paged = true;

            return GetList<Group>(builder.BuildUri());
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserGroupsPath, user.id);
            builder.Paged = true;

            return await GetListAsync<Group>(builder.BuildUri());
        }

        public IEnumerable<Playlist> GetPlaylists(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserPlaylistsPath, user.id);
            builder.Paged = true;

            return GetList<Playlist>(builder.BuildUri());
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserPlaylistsPath, user.id);
            builder.Paged = true;

            return await GetListAsync<Playlist>(builder.BuildUri());
        }

        public IEnumerable<Track> GetTracks(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserTracksPath, user.id);
            builder.Paged = true;

            return GetList<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserTracksPath, user.id);
            builder.Paged = true;

            return await GetListAsync<Track>(builder.BuildUri());
        }

        public IEnumerable<WebProfile> GetWebProfiles(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserWebProfilesPath, user.id);
            builder.Paged = true;

            return GetList<WebProfile>(builder.BuildUri());
        }

        public async Task<IEnumerable<WebProfile>> GetWebProfilesAsync(User user)
        {
            EnsureClientId();
            Validate(user.ValidateGet);

            var builder = new UserQueryBuilder();
            builder.Path = string.Format(UserWebProfilesPath, user.id);
            builder.Paged = true;

            return await GetListAsync<WebProfile>(builder.BuildUri());
        }
    }
}