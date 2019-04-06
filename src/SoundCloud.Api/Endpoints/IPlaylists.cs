using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Endpoints
{
    public interface IPlaylists
    {
        /// <summary>
        ///     Deletes the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <exception cref="System.Net.Http.HttpRequestException">Thrown if the WebRequest failed.</exception>
        Task<StatusResponse> DeleteAsync(Playlist playlist);

        /// <summary>
        ///     Gets a playlist
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        Task<Playlist> GetAsync(int playlistId);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <returns></returns>
        Task<IEnumerable<Playlist>> GetAsync(PlaylistQueryBuilder queryBuilder);

        /// <summary>
        ///     Gets the secret token of the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        Task<SecretToken> GetSecretTokenAsync(Playlist playlist);

        /// <summary>
        ///     Posts the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist" /> failed.</exception>
        Task<Playlist> PostAsync(Playlist playlist);

        /// <summary>
        ///     Updates the playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist" /> failed.</exception>
        Task<Playlist> UpdateAsync(Playlist playlist);

        /// <summary>
        ///     Uploads the given file as artwork to the given playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist" /> failed.</exception>
        Task<Playlist> UploadArtworkAsync(Playlist playlist, Stream file);
    }
}