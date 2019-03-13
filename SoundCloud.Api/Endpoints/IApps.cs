using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    /// Fetch metadata about a registered client application.
    /// </summary>
    public interface IApps
    {
        /// <summary>
        /// Gets a app
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        AppClient Get(int appId);

        /// <summary>
        /// Gets a app
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<AppClient> GetAsync(int appId);

        /// <summary>
        /// Gets a list of apps
        /// </summary>
        /// <returns></returns>
        ///  <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        IEnumerable<AppClient> Get();

        /// <summary>
        /// Gets a list of apps
        /// </summary>
        /// <returns></returns>
        ///  <exception cref="SoundCloudInsufficientAccessRightsException">Thrown if no ClientId or OAuth token is set.</exception>
        Task<IEnumerable<AppClient>> GetAsync();
    }
}