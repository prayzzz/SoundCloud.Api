using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;

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
        Task<StatusResponse> DeleteAsync(Comment comment);

        /// <summary>
        ///     Gets a list of comments
        /// </summary>
        Task<SoundCloudList<Comment>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit);

        /// <summary>
        ///     Gets a comment
        /// </summary>
        Task<Comment> GetAsync(long id);

        /// <summary>
        ///     Posts the given comment
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="comment" /> failed.</exception>
        Task<Comment> PostAsync(Comment comment);
    }
}
