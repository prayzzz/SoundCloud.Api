using System.Collections.Generic;
using System.IO;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

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
        IWebResult Delete(Playlist playlist);

        /// <summary>
        /// Gets a playlist
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Playlist Get(int playlistId);

        /// <summary>
        /// Gets a list of playlists
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Playlist> Get(PlaylistQueryBuilder queryBuilder);

        /// <summary>
        /// Gets the secret token of the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        SecretToken GetSecretToken(Playlist playlist);

        /// <summary>
        /// Posts the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist"/> failed.</exception>
        IWebResult<Playlist> Post(Playlist playlist);

        /// <summary>
        /// Updates the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist"/> failed.</exception>
        IWebResult<Playlist> Update(Playlist playlist);

        /// <summary>
        /// Uploads the given file as artwork to the given playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist"/> failed.</exception>
        IWebResult<Playlist> UploadArtwork(Playlist playlist, Stream file);
    }
}