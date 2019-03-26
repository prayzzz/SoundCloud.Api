using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a activity which is displayed in the activity stream.
    ///     The affected entity is stored in the <see cref="Origin" /> property.
    /// </summary>
    public sealed class Activity : Entity
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
        [JsonProperty("origin")]
        [JsonConverter(typeof(SoundCloudEntityJsonConverter))]
        public Entity Origin { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("tags")]
        [JsonIgnoreOnSerialize]
        public object Tags { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("type")]
        [JsonIgnoreOnSerialize]
        public ActivityType Type { get; set; }
    }
}