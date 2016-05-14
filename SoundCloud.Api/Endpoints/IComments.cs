using System.Collections.Generic;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Web;

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
        /// <exception cref="HttpException">Thrown if the WebRequest failed. Contains HttpStatusCode and StatusDescription</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        IWebResult Delete(Comment comment);

        /// <summary>
        /// Gets a list of comments
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<Comment> Get();

        /// <summary>
        /// Gets a comment
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Comment Get(int commentId);

        /// <summary>
        /// Posts the given comment
        /// </summary>
        /// <param name="comment"></param>
        /// <exception cref="HttpException">Thrown if the WebRequest failed. Contains HttpStatusCode and StatusDescription</exception>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no OAuth token is set.</exception>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="comment"/> failed.</exception>
        IWebResult<Comment> Post(Comment comment);
    }
}