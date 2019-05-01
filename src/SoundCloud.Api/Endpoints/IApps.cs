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
        Task<AppClient> GetAsync(long id);

        /// <summary>
        ///     Gets a list of apps
        /// </summary>
        Task<SoundCloudList<AppClient>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit);
    }
}
