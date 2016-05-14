using System.Collections.Generic;
using System.IO;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    public interface ITracks
    {
        /// <summary>
        /// Deletes the given track
        /// </summary>
        /// <param name="track">Track to delete</param>
        /// <exception cref="HttpException">Thrown if the WebRequest failed. Contains HttpStatusCode and StatusDescription</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IWebResult Delete(Track track);

        /// <summary>
        /// Gets a track
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Track Get(int trackId);

        /// <summary>
        /// Gets a list of tracks
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Track> Get();

        /// <summary>
        /// Gets a list of tracks
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Track> Get(SoundCloudQueryBuilder builder);

        /// <summary>
        /// Gets comments for the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Comment> GetComments(Track track);

        /// <summary>
        /// Gets users who favorited the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<User> GetFavoriters(Track track);

        /// <summary>
        /// Gets the secret token of the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        SecretToken GetSecretToken(Track track);

        /// <summary>
        /// Updates a track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="track"/> failed.</exception>
        IWebResult<Track> Update(Track track);

        /// <summary>
        /// Uploads a Artwork
        /// </summary>
        /// <param name="track"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="track"/> failed.</exception>
        IWebResult<Track> UploadArtwork(Track track, Stream file);

        /// <summary>
        /// Uploads a track
        /// </summary>
        /// <param name="title"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="title"/> failed.</exception>
        IWebResult<Track> UploadTrack(string title, Stream file);
    }
}