using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.Endpoints
{
    public interface IOAuth2
    {
        /// <summary>
        ///     Use client_id and client_secret to obtain an access_token
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> ClientCredentialsAsync(Credentials credentials);

        /// <summary>
        ///     Use this method to obtain an access_token after receiving a code via the authorization workflow.
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> ExchangeTokenAsync(Credentials credentials);

        /// <summary>
        ///     Use username and password to obtain an access_token
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> LoginAsync(Credentials credentials);

        /// <summary>
        ///     Use the refresh_token to obtain a new access_token
        /// </summary>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> RefreshTokenAsync(Credentials credentials);
    }
}