using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class OAuth2 : Endpoint, IOAuth2
    {
        private const string TokenPath = "oauth2/token?";

        public OAuth2(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<IWebResult<Credentials>> ClientCredentialsAsync(Credentials credentials)
        {
            Validate(credentials.ValidateClientCredentials);

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await CreateAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.ClientCredentials));
        }

        public async Task<IWebResult<Credentials>> ExchangeTokenAsync(Credentials credentials)
        {
            Validate(credentials.ValidateAuthorizationCode);

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await CreateAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.AuthorizationCode));
        }

        public async Task<IWebResult<Credentials>> LoginAsync(Credentials credentials)
        {
            Validate(credentials.ValidatePassword);

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await CreateAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.Password));
        }

        public async Task<IWebResult<Credentials>> RefreshTokenAsync(Credentials credentials)
        {
            Validate(credentials.ValidateRefreshToken);

            var builder = new OAuthQueryBuilder { Path = TokenPath };
            return await CreateAsync<Credentials>(builder.BuildUri(), credentials.ToParameters(GrantType.RefreshToken));
        }
    }
}