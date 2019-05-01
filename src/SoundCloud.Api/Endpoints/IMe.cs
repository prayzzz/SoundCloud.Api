using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    ///     The me resource allows you to get information about the authenticated user and easily access
    ///     his or her related sub-resources like tracks, followings, followers, groups and so on.
    ///     <para>
    ///         Use the WebProfile methods with care, the API seems to don't work correctly here.
    ///     </para>
    /// </summary>
    public interface IMe
    {
        /// <summary>
        ///     Deletes a web profile
        /// </summary>
        Task<StatusResponse> DeleteWebProfileAsync(WebProfile profile);

        /// <summary>
        ///     Follows the given user
        /// </summary>
        Task<StatusResponse> FollowAsync(User user);

        /// <summary>
        ///     Gets the current user
        /// </summary>
        Task<User> GetAsync();

        /// <summary>
        ///     Gets a list of activities
        /// </summary>
        Task<SoundCloudList<Activity>> GetActivitiesAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of comments
        ///     Note: This API seems to be broken. It returns all comments, instead of comments owned by the current user
        /// </summary>
        Task<SoundCloudList<Comment>> GetCommentsAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of connected external profiles
        /// </summary>
        Task<SoundCloudList<Connection>> GetConnectionsAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of favorites
        /// </summary>
        /// <returns></returns>
        Task<SoundCloudList<Track>> GetFavoritesAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of followers
        /// </summary>
        Task<SoundCloudList<User>> GetFollowersAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of followed users
        /// </summary>
        Task<SoundCloudList<User>> GetFollowingsAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        /// <returns></returns>
        Task<SoundCloudList<Playlist>> GetPlaylistsAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of tracks
        /// </summary>
        Task<SoundCloudList<Track>> GetTracksAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of web profiles
        /// </summary>
        Task<SoundCloudList<WebProfile>> GetWebProfilesAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Likes a track
        /// </summary>
        /// <param name="track"></param>
        Task<StatusResponse> LikeAsync(Track track);

        /// <summary>
        ///     Creates a web profile
        /// </summary>
        Task<WebProfile> PostWebProfileAsync(WebProfile profile);

        /// <summary>
        ///     Unfollows a user
        /// </summary>
        Task<StatusResponse> UnfollowAsync(User user);

        /// <summary>
        ///     Unlikes a track
        /// </summary>
        Task<StatusResponse> UnlikeAsync(Track track);
    }
}
