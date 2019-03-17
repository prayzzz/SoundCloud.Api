//ReSharper disable All

using System;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents an registered application
    /// </summary>
    public sealed class AppClient : Entity
    {
        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string name { get; set; }

        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string permalink_url { get; set; }

        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri uri { get; set; }
    }
}