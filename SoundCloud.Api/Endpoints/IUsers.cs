using System.Collections.Generic;

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
        User Get(int userId);

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> Get();

        /// <summary>
        /// Gets a list of users
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        IEnumerable<User> Get(UserQueryBuilder queryBuilder);

        /// <summary>
        /// Gets a list of comments from this user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Comment> GetComments(User user);

        /// <summary>
        /// Gets a list of tracks favorited by the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Track> GetFavorites(User user);

        /// <summary>
        /// Gets a list of users who are following the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<User> GetFollowers(User user);

        /// <summary>
        /// Gets a list of users who are followed by the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<User> GetFollowings(User user);

        /// <summary>
        /// Gets a list of joined groups
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Group> GetGroups(User user);

        /// <summary>
        /// Gets a list of playlists (sets) of the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Playlist> GetPlaylists(User user);

        /// <summary>
        /// Gets a list of tracks of the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<Track> GetTracks(User user);

        /// <summary>
        /// Gets a list of web profiles
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IEnumerable<WebProfile> GetWebProfiles(User user);
    }
}