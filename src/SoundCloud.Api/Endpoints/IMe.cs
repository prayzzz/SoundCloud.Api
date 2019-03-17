using SoundCloud.Api.Entities;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    /// The me resource allows you to get information about the authenticated user and easily access 
    /// his or her related sub-resources like tracks, followings, followers, groups and so on.
    /// 
    /// <para>
    /// Use the WebProfile methods with care, the API seems to don't work correctly here.
    /// </para>
    /// </summary>
    public interface IMe
    {
        /// <summary>
        /// Deletes a web profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        Task<IWebResult> DeleteWebProfileAsync(WebProfile profile);

        /// <summary>
        /// Follows the given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IWebResult> FollowAsync(User user);

        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns></returns>
        Task<User> GetAsync();

        /// <summary>
        /// Gets a list of activities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Activity>> GetActivitiesAsync();

        /// <summary>
        /// Gets a list of comments
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetCommentsAsync();

        /// <summary>
        /// Gets a list of connected external profiles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Connection>> GetConnectionsAsync();

        /// <summary>
        /// Gets a list of favorites
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetFavoritesAsync();

        /// <summary>
        /// Gets a list of followers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetFollowersAsync();

        /// <summary>
        /// Gets a list of followed users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetFollowingsAsync();

        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetGroupsAsync();

        /// <summary>
        /// Gets a list of playlists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Playlist>> GetPlaylistsAsync();

        /// <summary>
        /// Gets a list of tracks
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetTracksAsync();

        /// <summary>
        /// Gets a list of web profiles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WebProfile>> GetWebProfilesAsync();

        /// <summary>
        /// Likes a track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<IWebResult> LikeAsync(Track track);

        /// <summary>
        /// Creates a web profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        Task<IWebResult<WebProfile>> PostWebProfileAsync(WebProfile profile);

        /// <summary>
        /// Unfollows a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IWebResult> UnfollowAsync(User user);

        /// <summary>
        /// Unlikes a track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<IWebResult> UnlikeAsync(Track track);
    }
}