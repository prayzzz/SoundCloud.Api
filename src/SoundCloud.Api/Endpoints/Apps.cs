using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Endpoints
{
    internal class Apps : Endpoint, IApps
    {
        private const string AppPath = "apps/{0}?";
        private const string AppsPath = "apps?";

        public Apps(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<AppClient> GetAsync(int appId)
        {
            var builder = new AppsQueryBuilder { Path = string.Format(AppPath, appId) };
            return await GetByIdAsync<AppClient>(builder.BuildUri());
        }

        public async Task<IEnumerable<AppClient>> GetAsync()
        {
            var builder = new AppsQueryBuilder { Path = AppsPath, Paged = true };
            return await GetListAsync<AppClient>(builder.BuildUri());
        }
    }
}