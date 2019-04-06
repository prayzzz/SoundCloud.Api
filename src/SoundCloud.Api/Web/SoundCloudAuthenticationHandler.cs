using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Web
{
    public class SoundCloudAuthenticationHandler : DelegatingHandler
    {
        private readonly SoundCloudCredentials _credentials;

        public SoundCloudAuthenticationHandler(SoundCloudCredentials credentials)
        {
            _credentials = credentials;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = AppendCredentials(request.RequestUri, _credentials);
            return await base.SendAsync(request, cancellationToken);
        }

        private static Uri AppendCredentials(Uri uri, SoundCloudCredentials credentials)
        {
            if (uri == null)
            {
                return null;
            }


            if (uri.Query.Contains("oauth_token") || uri.Query.Contains("client_id"))
            {
                return uri;
            }

            var delimiter = "&";
            if (string.IsNullOrEmpty(uri.Query))
            {
                delimiter = "?";
            }
            else if (uri.Query.Last() == '?')
            {
                delimiter = "";
            }

            var uriString = uri.ToString();
            if (!string.IsNullOrEmpty(credentials.AccessToken))
            {
                uriString += delimiter + "oauth_token=" + credentials.AccessToken;
                return new Uri(uriString);
            }

            if (!string.IsNullOrEmpty(credentials.ClientId))
            {
                uriString += delimiter + "client_id=" + credentials.ClientId;
                return new Uri(uriString);
            }

            return new Uri(uriString);
        }
    }
}