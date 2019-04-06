using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Login;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents different the credentials used for authentication.
    /// </summary>
    public sealed class Credentials : Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        ///     Available for POST requests
        ///     The client id belonging to your application
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        ///     Available for POST requests
        ///     The client secret belonging to your application
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        ///     Available for POST requests
        ///     The authorization code obtained when user is sent to redirect_uri
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }

        /// <summary>
        ///     Available for POST requests
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        ///     Available for POST requests
        ///     The redirect uri you have configured for your application
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        ///     Available for POST requests
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("scope")]
        public Scope Scope { get; set; }

        /// <summary>
        ///     Available for POST requests
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        public void ValidateAuthorizationCode()
        {
            var messages = new ValidationMessages();

            if (string.IsNullOrEmpty(ClientId))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
            }

            if (string.IsNullOrEmpty(ClientSecret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
            }

            if (string.IsNullOrEmpty(Code))
            {
                messages.Add("Code missing. Use the code property to set the Code.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidateClientCredentials()
        {
            var messages = new ValidationMessages();

            if (string.IsNullOrEmpty(ClientId))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
            }

            if (string.IsNullOrEmpty(ClientSecret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidatePassword()
        {
            var messages = new ValidationMessages();

            if (string.IsNullOrEmpty(ClientId))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
            }

            if (string.IsNullOrEmpty(ClientSecret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
            }

            if (string.IsNullOrEmpty(Username))
            {
                messages.Add("Username missing. Use the username property to set the Username.");
            }

            if (string.IsNullOrEmpty(Password))
            {
                messages.Add("Password missing. Use the password property to set the Password.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidateRefreshToken()
        {
            var messages = new ValidationMessages();

            if (string.IsNullOrEmpty(ClientId))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
            }

            if (string.IsNullOrEmpty(ClientSecret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
            }

            if (string.IsNullOrEmpty(RefreshToken))
            {
                messages.Add("RefreshToken missing. Use the refresh_token property to set the RefreshToken.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        internal IDictionary<string, object> ToParameters(GrantType type)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("grant_type", type.GetAttributeOfType<EnumMemberAttribute>().Value);

            switch (type)
            {
                case GrantType.RefreshToken:
                    parameters.Add("client_id", ClientId);
                    parameters.Add("client_secret", ClientSecret);
                    parameters.Add("refresh_token", RefreshToken);
                    break;
                case GrantType.Password:
                    parameters.Add("client_id", ClientId);
                    parameters.Add("client_secret", ClientSecret);
                    parameters.Add("username", Username);
                    parameters.Add("password", Password);
                    break;
                case GrantType.ClientCredentials:
                    parameters.Add("client_id", ClientId);
                    parameters.Add("client_secret", ClientSecret);
                    break;
                case GrantType.AuthorizationCode:
                    parameters.Add("client_id", ClientId);
                    parameters.Add("client_secret", ClientSecret);
                    parameters.Add("code", Code);
                    break;
                default:
                    return parameters;
            }

            return parameters;
        }
    }
}