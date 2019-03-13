using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Threading.Tasks;

namespace SoundCloud.Api.Endpoints
{
    internal class Resolve : Endpoint, IResolve
    {
        private const string ResolvePath = "resolve?url={0}";

        public Resolve(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public Entity GetEntity(string url)
        {
            EnsureClientId();

            var builder = new ResolveQueryBuilder();
            builder.Path = string.Format(ResolvePath, url);

            return GetById<Entity>(builder.BuildUri());
        }

        public async Task<Entity> GetEntityAsync(string url)
        {
            EnsureClientId();

            var builder = new ResolveQueryBuilder();
            builder.Path = string.Format(ResolvePath, url);

            return await GetByIdAsync<Entity>(builder.BuildUri());
        }
    }
}