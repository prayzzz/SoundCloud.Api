using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Exceptions;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    /// The resolve resource allows you to lookup and access API resources when you only know the SoundCloud.com URL.
    /// </summary>
    public interface IResolve
    {
        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Entity GetEntity(string url);

        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<Entity> GetEntityAsync(string url);
    }
}