using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Endpoints
{
    public interface ITracks
    {
        /// <summary>
        ///     Deletes the given track
        /// </summary>
        /// <param name="track">Track to delete</param>
        /// <exception cref="System.Net.Http.HttpRequestException">Thrown if the WebRequest failed.</exception>
        Task<StatusResponse> DeleteAsync(Track track);

        /// <summary>
        ///     Gets a track
        /// </summary>
        /// <param name="trackId"></param>
        /// <returns></returns>
        Task<Track> GetAsync(int trackId);

        /// <summary>
        ///     Gets a list of tracks
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetAsync();

        /// <summary>
        ///     Gets a list of tracks
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        Task<IEnumerable<Track>> GetAsync(SoundCloudQueryBuilder builder);

        /// <summary>
        ///     Gets comments for the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetCommentsAsync(Track track);

        /// <summary>
        ///     Gets users who favorited the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetFavoritersAsync(Track track);

        /// <summary>
        ///     Gets the secret token of the track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<SecretToken> GetSecretTokenAsync(Track track);

        /// <summary>
        ///     Updates a track
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="track" /> failed.</exception>
        Task<Track> UpdateAsync(Track track);

        /// <summary>
        ///     Uploads a Artwork
        /// </summary>
        /// <param name="track"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="track" /> failed.</exception>
        Task<Track> UploadArtworkAsync(Track track, Stream file);

        /// <summary>
        ///     Uploads a track
        /// </summary>
        /// <param name="title"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="title" /> failed.</exception>
        Task<Track> UploadTrackAsync(string title, Stream file);
    }
}