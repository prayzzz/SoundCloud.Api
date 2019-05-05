using System;

namespace SoundCloud.Api.Utils
{
    public class SoundCloudAuthInfo
    {
        public SoundCloudAuthInfo(string accessToken, string clientId)
        {
            if (string.IsNullOrEmpty(accessToken) && string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException($"{nameof(accessToken)} or {nameof(clientId)} must not be null or empty");
            }

            AccessToken = accessToken;
            ClientId = clientId;
        }

        public string AccessToken { get; }

        public string ClientId { get; }
    }
}
