using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Endpoints
{
    public interface IUsers
    {
        /// <summary>
        ///     Gets a user
        /// </summary>
        Task<User> GetAsync(int userId);

        /// <summary>
        ///     Gets a list of users
        /// </summary>
        Task<IEnumerable<User>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of users
        /// </summary>
        Task<IEnumerable<User>> GetAsync(UserQueryBuilder queryBuilder);

        /// <summary>
        ///     Gets a list of comments from this user
        /// </summary>
        Task<IEnumerable<Comment>> GetCommentsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of tracks favorited by the user
        /// </summary>
        Task<IEnumerable<Track>> GetFavoritesAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of users who are following the user
        /// </summary>
        Task<IEnumerable<User>> GetFollowersAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of users who are followed by the user
        /// </summary>
        Task<IEnumerable<User>> GetFollowingsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of playlists (sets) of the user
        /// </summary>
        Task<IEnumerable<Playlist>> GetPlaylistsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of tracks of the user
        /// </summary>
        Task<IEnumerable<Track>> GetTracksAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of web profiles
        /// </summary>
        Task<IEnumerable<WebProfile>> GetWebProfilesAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);
    }
}