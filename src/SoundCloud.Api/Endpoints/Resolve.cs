using System.Threading.Tasks;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Resolve : Endpoint, IResolve
    {
        private const string ResolvePath = "resolve?url={0}";

        public Resolve(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<Entity> GetEntityAsync(string url)
        {
            var builder = new ResolveQueryBuilder { Path = string.Format(ResolvePath, url) };
            return await GetByIdAsync<Entity>(builder.BuildUri());
        }
    }
}