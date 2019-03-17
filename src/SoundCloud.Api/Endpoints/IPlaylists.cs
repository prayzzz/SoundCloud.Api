using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    public interface IPlaylists
    {
        /// <summary>
        /// Deletes the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed. Contains HttpStatusCode and StatusDescription</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IWebResult> DeleteAsync(Playlist playlist);

        /// <summary>
        /// Gets a playlist
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<Playlist> GetAsync(int playlistId);

        /// <summary>
        /// Gets a list of playlists
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Playlist>> GetAsync(PlaylistQueryBuilder queryBuilder);

        /// <summary>
        /// Gets the secret token of the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<SecretToken> GetSecretTokenAsync(Playlist playlist);

        /// <summary>
        /// Posts the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist"/> failed.</exception>
        Task<IWebResult<Playlist>> PostAsync(Playlist playlist);

        /// <summary>
        /// Updates the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist"/> failed.</exception>
        Task<IWebResult<Playlist>> UpdateAsync(Playlist playlist);

        /// <summary>
        /// Uploads the given file as artwork to the given playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist"/> failed.</exception>
        Task<IWebResult<Playlist>> UploadArtworkAsync(Playlist playlist, Stream file);
    }
}