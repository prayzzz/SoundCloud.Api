using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Endpoints
{
    public interface IUsers
    {
        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetAsync(int userId);

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAsync();

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetAsync(UserQueryBuilder queryBuilder);

        /// <summary>
        /// Gets a list of comments from this user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetCommentsAsync(User user);

        /// <summary>
        /// Gets a list of tracks favorited by the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetFavoritesAsync(User user);

        /// <summary>
        /// Gets a list of users who are following the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetFollowersAsync(User user);

        /// <summary>
        /// Gets a list of users who are followed by the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetFollowingsAsync(User user);

        /// <summary>
        /// Gets a list of playlists (sets) of the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Playlist>> GetPlaylistsAsync(User user);

        /// <summary>
        /// Gets a list of tracks of the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetTracksAsync(User user);

        /// <summary>
        /// Gets a list of web profiles
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IEnumerable<WebProfile>> GetWebProfilesAsync(User user);
    }
}