using System.Threading.Tasks;
using SoundCloud.Api.Entities.Base;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    ///     The resolve resource allows you to lookup and access API resources when you only know the SoundCloud.com URL.
    /// </summary>
    public interface IResolve
    {
        /// <summary>
        ///     Gets an entity
        /// </summary>
        Task<Entity> GetEntityAsync(string url);
    }
}