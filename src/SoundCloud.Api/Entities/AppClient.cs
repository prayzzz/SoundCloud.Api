using System;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents an registered application
    /// </summary>
    public sealed class AppClient : Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("name")]
        [JsonIgnoreOnSerialize]
        public string Name { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("uri")]
        [JsonIgnoreOnSerialize]
        public Uri Uri { get; set; }
    }
}