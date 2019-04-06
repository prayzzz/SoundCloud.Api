using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.Endpoints
{
    public interface IOAuth2
    {
        /// <summary>
        ///     Use client_id and client_secrect to obtain an access_token
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> ClientCredentialsAsync(Credentials credentials);

        /// <summary>
        ///     Use this method to obtain an access_token after recieving a code via the authorization workflow.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> ExchangeTokenAsync(Credentials credentials);

        /// <summary>
        ///     Use username and password to obtain an access_token
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> LoginAsync(Credentials credentials);

        /// <summary>
        ///     Use the refresh_token to obtain a new access_token
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="SoundCloudValidationException">Thrown if validation of <paramref name="credentials" /> failed.</exception>
        Task<Credentials> RefreshTokenAsync(Credentials credentials);
    }
}