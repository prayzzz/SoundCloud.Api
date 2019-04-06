using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Apps : IApps
    {
        private const string AppPath = "apps/{0}?";
        private const string AppsPath = "apps?";

        private readonly ISoundCloudApiGateway _gateway;

        public Apps(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AppClient> GetAsync(int appId)
        {
            var builder = new AppsQueryBuilder { Path = string.Format(AppPath, appId) };
            return await _gateway.SendGetRequestAsync<AppClient>(builder.BuildUri());
        }

        public async Task<IEnumerable<AppClient>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0)
        {
            var builder = new AppsQueryBuilder { Path = AppsPath, Paged = true, Limit = limit, Offset = offset };
            return (await _gateway.SendGetRequestAsync<PagedResult<AppClient>>(builder.BuildUri())).Collection;
        }
    }
}