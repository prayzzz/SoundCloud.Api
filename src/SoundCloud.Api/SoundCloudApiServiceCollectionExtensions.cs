using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SoundCloud.Api;
using SoundCloud.Api.Utils;
using SoundCloud.Api.Web;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class SoundCloudApiServiceCollectionExtensions
    {
        public static IServiceCollection AddSoundCloudClient(this IServiceCollection serviceCollection, string accessToken, string clientId)
        {
            serviceCollection.AddSingleton<SoundCloudClient>();

            serviceCollection.AddSoundCloudHttpClient(accessToken, clientId);

            return serviceCollection;
        }

        public static IHttpClientBuilder AddSoundCloudHttpClient(this IServiceCollection serviceCollection, string accessToken, string clientId)
        {
            var version = typeof(SoundCloudClient).Assembly.GetName().Version.ToString();
            var userAgent = new ProductInfoHeaderValue("SoundCloud.Api", version);

            serviceCollection.TryAddSingleton(new SoundCloudCredentials(accessToken, clientId));
            serviceCollection.TryAddTransient<AuthenticationHandler>();

            var builder = serviceCollection.AddHttpClient(SoundCloudClient.HttpClientName);
            builder.AddHttpMessageHandler<AuthenticationHandler>();
            builder.ConfigureHttpClient(c => { c.DefaultRequestHeaders.UserAgent.Add(userAgent); });

            return builder;
        }
    }
}