using System.Collections.Generic;

using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

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

        public AppClient Get(int appId)
        {
            EnsureClientId();

            var builder = new AppsQueryBuilder();
            builder.Path = string.Format(AppPath, appId);

            return GetById<AppClient>(builder.BuildUri());
        }

        public IEnumerable<AppClient> Get()
        {
            EnsureClientId();

            var builder = new AppsQueryBuilder();
            builder.Path = AppsPath;
            builder.Paged = true;

            return GetList<AppClient>(builder.BuildUri());
        }
    }
}