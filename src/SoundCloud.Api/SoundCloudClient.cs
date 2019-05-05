using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Utils;
using SoundCloud.Api.Web;

namespace SoundCloud.Api
{
    public sealed class SoundCloudClient : ISoundCloudClient
    {
        private const string ArgumentMustNotBeNullOrEmpty = "Argument must not be null or empty";
        public const string HttpClientName = "SoundCloud.Api";

        public SoundCloudClient(IHttpClientFactory httpClientFactory, SoundCloudAuthInfo authInfo)
        {
            AuthInfo = authInfo;

            var gateway = new SoundCloudApiGateway(httpClientFactory);
            Apps = new Apps(gateway);
            Comments = new Comments(gateway);
            OAuth2 = new OAuth2(gateway);
            Playlists = new Playlists(gateway);
            Tracks = new Tracks(gateway);
            Users = new Users(gateway);
            Me = new Me(gateway);
            Resolve = new Resolve(gateway);
        }

        public SoundCloudAuthInfo AuthInfo { get; }

        public IApps Apps { get; }

        public IComments Comments { get; }

        public IMe Me { get; }

        public IOAuth2 OAuth2 { get; }

        public IPlaylists Playlists { get; }

        public IResolve Resolve { get; }

        public ITracks Tracks { get; }

        public IUsers Users { get; }


        public static ISoundCloudClient CreateAuthorized(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException(ArgumentMustNotBeNullOrEmpty, nameof(accessToken));
            }

            return GetClient(new SoundCloudAuthInfo(accessToken, null));
        }

        public static ISoundCloudClient CreateUnauthorized(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException(ArgumentMustNotBeNullOrEmpty, nameof(clientId));
            }

            return GetClient(new SoundCloudAuthInfo(null, clientId));
        }

        private static SoundCloudClient GetClient(SoundCloudAuthInfo credentials)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSoundCloudClient(credentials);

            var provider = serviceCollection.BuildServiceProvider();
            return provider.GetService<SoundCloudClient>();
        }
    }
}
