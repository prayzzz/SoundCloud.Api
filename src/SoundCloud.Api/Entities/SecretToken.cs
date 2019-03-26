using System;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a secret token for a resource
    /// </summary>
    public sealed class SecretToken : Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("resource_uri")]
        public string ResourceUri { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("uri")]
        [JsonIgnoreOnSerialize]
        public Uri Uri { get; set; }
    }
}