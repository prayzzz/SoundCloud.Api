using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    /// Comments can be made on tracks by any user who has access to a track, except for non commentable tracks. 
    /// As you see in the SoundCloud player comments can also be associated with a specific timestamp in a track.
    /// </summary>
    public interface IComments
    {
        /// <summary>
        /// Deletes the given comment
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed. Contains HttpStatusCode and StatusDescription</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        Task<IWebResult> DeleteAsync(Comment comment);

        /// <summary>
        /// Gets a list of comments
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<Comment>> GetAsync();

        /// <summary>
        /// Gets a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<Comment> GetAsync(int commentId);

        /// <summary>
        /// Posts the given comment
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="System.Web.HttpException">Thrown if the WebRequest failed. Contains HttpStatusCode and StatusDescription</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="comment"/> failed.</exception>
        Task<IWebResult<Comment>> PostAsync(Comment comment);
    }
}