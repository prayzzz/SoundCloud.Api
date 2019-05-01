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
        Task<User> GetAsync(long userId);

        /// <summary>
        ///     Gets a list of users
        /// </summary>
        Task<SoundCloudList<User>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of users
        /// </summary>
        Task<SoundCloudList<User>> GetAllAsync(UserQueryBuilder builder);

        /// <summary>
        ///     Gets a list of comments from this user
        /// </summary>
        Task<SoundCloudList<Comment>> GetCommentsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of tracks favorited by the user
        /// </summary>
        Task<SoundCloudList<Track>> GetFavoritesAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of users who are following the user
        /// </summary>
        Task<SoundCloudList<User>> GetFollowersAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of users who are followed by the user
        /// </summary>
        Task<SoundCloudList<User>> GetFollowingsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of playlists (sets) of the user
        /// </summary>
        Task<SoundCloudList<Playlist>> GetPlaylistsAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of tracks of the user
        /// </summary>
        Task<SoundCloudList<Track>> GetTracksAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of web profiles
        /// </summary>
        Task<SoundCloudList<WebProfile>> GetWebProfilesAsync(User user, int limit = SoundCloudQueryBuilder.MaxLimit);
    }
}
