using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    ///     Comments can be made on tracks by any user who has access to a track, except for non commentable tracks.
    ///     As you see in the SoundCloud player comments can also be associated with a specific timestamp in a track.
    /// </summary>
    public interface IComments
    {
        /// <summary>
        ///     Deletes the given comment
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="System.Net.Http.HttpRequestException">Thrown if the WebRequest failed.</exception>
        Task<StatusResponse> DeleteAsync(Comment comment);

        /// <summary>
        ///     Gets a list of comments
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Comment>> GetAsync();

        /// <summary>
        ///     Gets a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        Task<Comment> GetAsync(int commentId);

        /// <summary>
        ///     Posts the given comment
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="System.Net.Http.HttpRequestException">Thrown if the WebRequest failed.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="comment" /> failed.</exception>
        Task<Comment> PostAsync(Comment comment);
    }
}