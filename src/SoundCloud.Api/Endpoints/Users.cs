using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Users : IUsers
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

        private readonly ISoundCloudApiGateway _gateway;

        public Users(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await GetAsync(new UserQueryBuilder());
        }

        public async Task<IEnumerable<User>> GetAsync(UserQueryBuilder queryBuilder)
        {
            queryBuilder.Path = UsersPath;
            queryBuilder.Paged = true;

            return (await _gateway.SendGetRequestAsync<PagedResult<User>>(queryBuilder.BuildUri())).Collection;
        }

        public async Task<User> GetAsync(int userId)
        {
            var builder = new UserQueryBuilder { Path = string.Format(UserPath, userId) };
            return await _gateway.SendGetRequestAsync<User>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserCommentsPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Comment>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Track>> GetFavoritesAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserFavoritesPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Track>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserFollowersPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<User>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<User>> GetFollowingsAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserFollowingsPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<User>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserPlaylistsPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Playlist>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserTracksPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<Track>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<WebProfile>> GetWebProfilesAsync(User user)
        {
            user.ValidateGet();

            var builder = new UserQueryBuilder { Path = string.Format(UserWebProfilesPath, user.Id), Paged = true };
            return (await _gateway.SendGetRequestAsync<PagedResult<WebProfile>>(builder.BuildUri())).Collection;
        }
    }
}