// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Runtime.Serialization;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Login;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents different the credentials used for authentication.
    /// </summary>
    public sealed class Credentials : Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// Available for POST requests
        /// The client id belonging to your application
        /// </summary>
        public string client_id { get; set; }

        /// <summary>
        /// Available for POST requests
        /// The client secret belonging to your application
        /// </summary>
        public string client_secret { get; set; }

        /// <summary>
        /// Available for POST requests
        /// The authorization code obtained when user is sent to redirect_uri
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public int? expires_in { get; set; }

        /// <summary>
        /// Available for POST requests
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Available for POST requests
        /// The redirect uri you have configured for your application
        /// </summary>
        public string redirect_uri { get; set; }

        /// <summary>
        /// Available for POST requests
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public Scope scope { get; set; }

        /// <summary>
        /// Available for POST requests
        /// </summary>
        public string username { get; set; }

        public bool ValidateAuthorizationCode(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(client_id))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
                return false;
            }

            if (string.IsNullOrEmpty(client_secret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
                return false;
            }

            if (string.IsNullOrEmpty(code))
            {
                messages.Add("Code missing. Use the code property to set the Code.");
                return false;
            }

            return true;
        }

        public bool ValidateClientCredentials(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(client_id))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
                return false;
            }

            if (string.IsNullOrEmpty(client_secret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
                return false;
            }

            return true;
        }

        public bool ValidatePassword(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(client_id))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
                return false;
            }

            if (string.IsNullOrEmpty(client_secret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
                return false;
            }

            if (string.IsNullOrEmpty(username))
            {
                messages.Add("Username missing. Use the username property to set the Username.");
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                messages.Add("Password missing. Use the password property to set the Password.");
                return false;
            }

            return true;
        }

        public bool ValidateRefreshToken(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(client_id))
            {
                messages.Add("ClientId missing. Use the client_id property to set the ClientId.");
                return false;
            }

            if (string.IsNullOrEmpty(client_secret))
            {
                messages.Add("ClientSecret missing. Use the client_secret property to set the ClientSecret.");
                return false;
            }

            if (string.IsNullOrEmpty(refresh_token))
            {
                messages.Add("RefreshToken missing. Use the refresh_token property to set the RefreshToken.");
                return false;
            }

            return true;
        }

        internal IDictionary<string, object> ToParameters(GrantType type)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("grant_type", type.GetAttributeOfType<EnumMemberAttribute>().Value);

            switch (type)
            {
                case GrantType.RefreshToken:
                    parameters.Add("client_id", client_id);
                    parameters.Add("client_secret", client_secret);
                    parameters.Add("refresh_token", refresh_token);
                    break;
                case GrantType.Password:
                    parameters.Add("client_id", client_id);
                    parameters.Add("client_secret", client_secret);
                    parameters.Add("username", username);
                    parameters.Add("password", password);
                    break;
                case GrantType.ClientCredentials:
                    parameters.Add("client_id", client_id);
                    parameters.Add("client_secret", client_secret);
                    break;
                case GrantType.AuthorizationCode:
                    parameters.Add("client_id", client_id);
                    parameters.Add("client_secret", client_secret);
                    parameters.Add("code", code);
                    break;
                default:
                    return parameters;
            }

            return parameters;
        }
    }
}