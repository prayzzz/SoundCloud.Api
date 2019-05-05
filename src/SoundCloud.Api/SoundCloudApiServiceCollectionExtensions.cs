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
        public static IServiceCollection AddSoundCloudClient(this IServiceCollection serviceCollection, SoundCloudAuthInfo credentials)
        {
            serviceCollection.AddSingleton<SoundCloudClient>();

            serviceCollection.AddSoundCloudHttpClient(credentials);

            return serviceCollection;
        }

        public static IHttpClientBuilder AddSoundCloudHttpClient(this IServiceCollection serviceCollection, SoundCloudAuthInfo credentials)
        {
            var version = typeof(SoundCloudClient).Assembly.GetName().Version.ToString();
            var userAgent = new ProductInfoHeaderValue("SoundCloud.Api", version);

            serviceCollection.TryAddSingleton(credentials);
            serviceCollection.TryAddTransient<SoundCloudAuthenticationHandler>();

            var builder = serviceCollection.AddHttpClient(SoundCloudClient.HttpClientName);
            builder.AddHttpMessageHandler<SoundCloudAuthenticationHandler>();
            builder.ConfigureHttpClient(c => { c.DefaultRequestHeaders.UserAgent.Add(userAgent); });

            return builder;
        }
    }
}
