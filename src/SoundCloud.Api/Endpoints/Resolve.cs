using System.Threading.Tasks;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Resolve : IResolve
    {
        private const string ResolvePath = "resolve?url={0}";

        private readonly ISoundCloudApiGateway _gateway;

        public Resolve(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<Entity> GetEntityAsync(string url)
        {
            var builder = new ResolveQueryBuilder { Path = string.Format(ResolvePath, url) };
            return await _gateway.SendGetRequestAsync<Entity>(builder.BuildUri());
        }
    }
}