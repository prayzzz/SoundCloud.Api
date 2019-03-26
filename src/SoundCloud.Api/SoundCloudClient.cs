using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Web;

namespace SoundCloud.Api
{
    public sealed class SoundCloudClient : ISoundCloudClient
    {
        private const string ArgumentMustNotBeNullOrEmpty = "Argument must not be null or empty";
        public const string HttpClientName = "SoundCloud.Api";

        private readonly Apps _apps;
        private readonly Comments _comments;
        private readonly Me _me;
        private readonly OAuth2 _oAuth2;
        private readonly Playlists _playlists;
        private readonly Resolve _resolve;
        private readonly Tracks _tracks;
        private readonly Users _users;

        public SoundCloudClient(IHttpClientFactory httpClientFactory)
        {
            var gateway = new SoundCloudApiGateway(httpClientFactory);
            _comments = new Comments(gateway);
            _oAuth2 = new OAuth2(gateway);
            _playlists = new Playlists(gateway);
            _tracks = new Tracks(gateway);
            _users = new Users(gateway);
            _me = new Me(gateway);
            _apps = new Apps(gateway);
            _resolve = new Resolve(gateway);
        }

        public IApps Apps => _apps;

        public IComments Comments => _comments;

        public IMe Me => _me;

        public IOAuth2 OAuth2 => _oAuth2;

        public IPlaylists Playlists => _playlists;

        public IResolve Resolve => _resolve;

        public ITracks Tracks => _tracks;

        public IUsers Users => _users;

        public static ISoundCloudClient CreateAuthorized(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException(ArgumentMustNotBeNullOrEmpty, nameof(accessToken));
            }

            var httpClientFactory = GetDefaultHttpClientFactory(accessToken, null);
            return new SoundCloudClient(httpClientFactory);
        }

        public static ISoundCloudClient CreateUnauthorized(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException(ArgumentMustNotBeNullOrEmpty, nameof(clientId));
            }

            var httpClientFactory = GetDefaultHttpClientFactory(null, clientId);
            return new SoundCloudClient(httpClientFactory);
        }

        private static IHttpClientFactory GetDefaultHttpClientFactory(string accessToken, string clientId)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSoundCloudHttpClient(accessToken, clientId);

            var provider = serviceCollection.BuildServiceProvider();
            var httpClientFactory = provider.GetService<IHttpClientFactory>();
            return httpClientFactory;
        }
    }
}