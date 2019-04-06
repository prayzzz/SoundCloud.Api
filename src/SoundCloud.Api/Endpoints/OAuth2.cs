using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class OAuth2 : IOAuth2
    {
        private const string TokenPath = "oauth2/token?";

        private readonly ISoundCloudApiGateway _gateway;

        public OAuth2(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<Credentials> ClientCredentialsAsync(Credentials credentials)
        {
            credentials.ValidateClientCredentials();

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await _gateway.SendPostRequestAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.ClientCredentials));
        }

        public async Task<Credentials> ExchangeTokenAsync(Credentials credentials)
        {
            credentials.ValidateAuthorizationCode();

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await _gateway.SendPostRequestAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.AuthorizationCode));
        }

        public async Task<Credentials> LoginAsync(Credentials credentials)
        {
            credentials.ValidatePassword();

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await _gateway.SendPostRequestAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.Password));
        }

        public async Task<Credentials> RefreshTokenAsync(Credentials credentials)
        {
            credentials.ValidateRefreshToken();

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await _gateway.SendPostRequestAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.RefreshToken));
        }
    }
}