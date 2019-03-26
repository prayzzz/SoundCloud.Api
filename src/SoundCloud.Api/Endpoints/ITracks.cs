using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    public interface ITracks
    {
        /// <summary>
        ///     Deletes the given track
        /// </summary>
        /// <param name="track">Track to delete</param>
        /// <exception cref="System.Net.Http.HttpRequestException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IWebResult> DeleteAsync(Track track);

        /// <summary>
        ///     Gets a track
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<Track> GetAsync(int trackId);

        /// <summary>
        ///     Gets a list of tracks
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Track>> GetAsync();

        /// <summary>
        ///     Gets a list of tracks
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Track>> GetAsync(SoundCloudQueryBuilder builder);

        /// <summary>
        ///     Gets comments for the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Comment>> GetCommentsAsync(Track track);

        /// <summary>
        ///     Gets users who favorited the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<User>> GetFavoritersAsync(Track track);

        /// <summary>
        ///     Gets the secret token of the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<SecretToken> GetSecretTokenAsync(Track track);

        /// <summary>
        ///     Updates a track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="track" /> failed.</exception>
        Task<IWebResult<Track>> UpdateAsync(Track track);

        /// <summary>
        ///     Uploads a Artwork
        /// </summary>
        /// <param name="track"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="track" /> failed.</exception>
        Task<IWebResult<Track>> UploadArtworkAsync(Track track, Stream file);

        /// <summary>
        ///     Uploads a track
        /// </summary>
        /// <param name="title"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="title" /> failed.</exception>
        Task<IWebResult<Track>> UploadTrackAsync(string title, Stream file);
    }
}