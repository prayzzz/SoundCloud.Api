using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    /// Groups have members and contributed tracks.
    /// </summary>
    public interface IGroups
    {
        /// <summary>
        /// Deletes a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed.</exception>
        Task<IWebResult> DeleteAsync(Group group);

        /// <summary>
        /// Deletes a contributed track
        /// </summary>
        /// <param name="group"></param>
        /// <param name="track"></param>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed.</exception>
        Task<IWebResult> DeleteContributionAsync(Group group, Track track);

        /// <summary>
        /// Deletes a pending track
        /// </summary>
        /// <param name="group"></param>
        /// <param name="track"></param>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed.</exception>
        Task<IWebResult> DeletePendingTrackAsync(Group group, Track track);

        /// <summary>
        /// Gets a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<Group> GetAsync(int groupId);

        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Group>> GetAsync();

        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Group>> GetAsync(GroupQueryBuilder queryBuilder);

        /// <summary>
        /// list of contributed tracks (for moderators). POST creates contribution
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IEnumerable<Track>> GetContributionsAsync(Group group);

        /// <summary>
        /// list of users who contributed a track to the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IEnumerable<User>> GetContributorsAsync(Group group);

        /// <summary>
        /// list of users who joined the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<User>> GetMembersAsync(Group group);

        /// <summary>
        /// list of users who moderate the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<User>> GetModeratorsAsync(Group group);

        /// <summary>
        /// list of contributed but not approved tracks (for moderators)
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IEnumerable<Track>> GetPendingTracksAsync(Group group);

        /// <summary>
        /// list of contributed and approved tracks
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Track>> GetTracksAsync(Group group);

        /// <summary>
        /// list of users who contributed to, joined or moderate the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<User>> GetUsersAsync(Group group);

        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IWebResult<Group>> PostAsync(Group group);

        /// <summary>
        /// Posts a track to a group
        /// </summary>
        /// <param name="group"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<IWebResult<Track>> PostAsync(Group group, Track track);

        /// <summary>
        /// Updates a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IWebResult<Group>> UpdateAsync(Group group);

        /// <summary>
        /// Uploads a Artwork
        /// </summary>
        /// <param name="group"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IWebResult<Group>> UploadArtworkAsync(Group group, Stream file);
    }
}