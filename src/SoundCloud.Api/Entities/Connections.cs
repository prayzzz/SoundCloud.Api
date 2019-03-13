// ReSharper disable InconsistentNaming

using System;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a connection to a other social media platform
    /// </summary>
    public sealed class Connection : Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string created_at { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string display_name { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool post_favorite { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool post_publish { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public ConnectionService service { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public ConnectionService type { get; set; }

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