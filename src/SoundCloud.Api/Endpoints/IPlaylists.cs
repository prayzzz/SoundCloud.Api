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
        Task<Playlist> GetAsync(long playlistId);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        Task<SoundCloudList<Playlist>> GetAllAsync(string searchString, int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a list of playlists
        /// </summary>
        Task<SoundCloudList<Playlist>> GetAllAsync(PlaylistQueryBuilder builder);

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
