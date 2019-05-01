using System;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal abstract class Endpoint
    {
        protected readonly ISoundCloudApiGateway Gateway;

        internal Endpoint(ISoundCloudApiGateway gateway)
        {
            Gateway = gateway;
        }

        protected async Task<SoundCloudList<T>> GetPage<T>(Uri href) where T : Entity
        {
            var page = await Gateway.SendGetRequestAsync<PagedResult<T>>(href);
            if (page.HasNextPage)
            {
                return new SoundCloudList<T>(page.Collection, () => GetPage<T>(page.NextHref));
            }

            return new SoundCloudList<T>(page.Collection);
        }
    }
}
