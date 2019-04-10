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
        Task<StatusResponse> DeleteAsync(Playlist playlist);

        /// <summary>
        ///     Gets a playlist
        /// </summary>
        Task<Playlist> GetAsync(int playlistId);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        Task<IEnumerable<Playlist>> GetAllAsync(string searchString, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        Task<IEnumerable<Playlist>> GetAllAsync(PlaylistQueryBuilder queryBuilder);

        /// <summary>
        ///     Gets the secret token of the playlist
        /// </summary>
        Task<SecretToken> GetSecretTokenAsync(Playlist playlist);

        /// <summary>
        ///     Posts the playlist
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist" /> failed.</exception>
        Task<Playlist> PostAsync(Playlist playlist);

        /// <summary>
        ///     Updates the playlist
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist" /> failed.</exception>
        Task<Playlist> UpdateAsync(Playlist playlist);

        /// <summary>
        ///     Uploads the given file as artwork to the given playlist
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="playlist" /> failed.</exception>
        Task<Playlist> UploadArtworkAsync(Playlist playlist, Stream file);
    }
}