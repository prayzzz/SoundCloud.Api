using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Apps : Endpoint, IApps
    {
        private const string AppPath = "apps/{0}?";
        private const string AppsPath = "apps?";

        public Apps(ISoundCloudApiGateway gateway) : base(gateway)
        {
        }

        public async Task<AppClient> GetAsync(long id)
        {
            var builder = new AppsQueryBuilder { Path = string.Format(AppPath, id) };
            return await Gateway.SendGetRequestAsync<AppClient>(builder.BuildUri());
        }

        public Task<SoundCloudList<AppClient>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new AppsQueryBuilder { Path = AppsPath, Paged = true, Limit = limit };
            return GetPage<AppClient>(builder.BuildUri());
        }
    }
}
