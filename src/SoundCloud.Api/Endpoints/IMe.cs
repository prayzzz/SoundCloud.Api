using System.Collections.Generic;
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
        Task<IEnumerable<Activity>> GetActivitiesAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of comments
        /// </summary>
        Task<IEnumerable<Comment>> GetCommentsAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of connected external profiles
        /// </summary>
        Task<IEnumerable<Connection>> GetConnectionsAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of favorites
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetFavoritesAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of followers
        /// </summary>
        Task<IEnumerable<User>> GetFollowersAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of followed users
        /// </summary>
        Task<IEnumerable<User>> GetFollowingsAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Playlist>> GetPlaylistsAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of tracks
        /// </summary>
        Task<IEnumerable<Track>> GetTracksAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of web profiles
        /// </summary>
        Task<IEnumerable<WebProfile>> GetWebProfilesAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

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