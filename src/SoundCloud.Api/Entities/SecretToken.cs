// ReSharper disable InconsistentNaming

using System;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a secret token for a resource
    /// </summary>
    public sealed class SecretToken : Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string resource_uri { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string token { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri uri { get; set; }

        internal override void AppendCredentialsToProperties(SoundCloudCredentials credentials)
        {
            uri = uri.AppendCredentials(credentials);
        }
    }
}