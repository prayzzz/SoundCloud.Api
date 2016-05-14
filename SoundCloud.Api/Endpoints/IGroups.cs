using System.Collections.Generic;
using System.IO;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

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
        /// <exception cref="HttpException">Thrown if the WebRequest failed.</exception>
        IWebResult Delete(Group group);

        /// <summary>
        /// Deletes a contributed track
        /// </summary>
        /// <param name="group"></param>
        /// <param name="track"></param>
        /// <exception cref="HttpException">Thrown if the WebRequest failed.</exception>
        IWebResult DeleteContribution(Group group, Track track);

        /// <summary>
        /// Deletes a pending track
        /// </summary>
        /// <param name="group"></param>
        /// <param name="track"></param>
        /// <exception cref="HttpException">Thrown if the WebRequest failed.</exception>
        IWebResult DeletePendingTrack(Group group, Track track);

        /// <summary>
        /// Gets a group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Group Get(int groupId);

        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Group> Get();

        /// <summary>
        /// Gets a list of groups
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Group> Get(GroupQueryBuilder queryBuilder);

        /// <summary>
        /// list of contributed tracks (for moderators). POST creates contribution
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IEnumerable<Track> GetContributions(Group group);

        /// <summary>
        /// list of users who contributed a track to the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IEnumerable<User> GetContributors(Group group);

        /// <summary>
        /// list of users who joined the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<User> GetMembers(Group group);

        /// <summary>
        /// list of users who moderate the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<User> GetModerators(Group group);

        /// <summary>
        /// list of contributed but not approved tracks (for moderators)
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IEnumerable<Track> GetPendingTracks(Group group);

        /// <summary>
        /// list of contributed and approved tracks
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Track> GetTracks(Group group);

        /// <summary>
        /// list of users who contributed to, joined or moderate the group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<User> GetUsers(Group group);

        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="HttpException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IWebResult<Group> Post(Group group);

        /// <summary>
        /// Posts a track to a group
        /// </summary>
        /// <param name="group"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        IWebResult<Track> Post(Group group, Track track);

        /// <summary>
        /// Updates a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        /// <exception cref="HttpException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IWebResult<Group> Update(Group group);

        /// <summary>
        /// Uploads a Artwork
        /// </summary>
        /// <param name="group"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="HttpException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IWebResult<Group> UploadArtwork(Group group, Stream file);
    }
}