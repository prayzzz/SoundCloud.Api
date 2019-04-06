using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;

namespace SoundCloud.Api.Endpoints
{
    /// <summary>
    ///     Fetch metadata about a registered client application.
    /// </summary>
    public interface IApps
    {
        /// <summary>
        ///     Gets a app
        /// </summary>
        /// <param name="appId"></param>
        Task<AppClient> GetAsync(int appId);

        /// <summary>
        ///     Gets a list of apps
        /// </summary>
        Task<IEnumerable<AppClient>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0);
    }
}