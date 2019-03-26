using System;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a connection to a other social media platform
    /// </summary>
    public sealed class Connection : Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("post_favorite")]
        public bool PostFavorite { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("post_publish")]
        public bool PostPublish { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("service")]
        public ConnectionService Service { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("type")]
        [JsonIgnoreOnSerialize]
        public ConnectionService Type { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("uri")]
        [JsonIgnoreOnSerialize]
        public Uri Uri { get; set; }
    }
}