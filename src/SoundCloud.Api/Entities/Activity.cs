// ReSharper disable InconsistentNaming

using Newtonsoft.Json;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a activity which is displayed in the activity stream.
    /// The affected entity is stored in the <see cref="origin"/> property.
    /// </summary>
    public sealed class Activity : Entity
    {
        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string created_at { get; set; }

        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonConverter(typeof(SoundCloudEntityJsonConverter))]
        public Entity origin { get; set; }

        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        public object tags { get; set; }

        /// <summary>
        /// Available for GET requets
        /// </summary>
        [JsonIgnoreOnSerialize]
        public ActivityType type { get; set; }
    }
}