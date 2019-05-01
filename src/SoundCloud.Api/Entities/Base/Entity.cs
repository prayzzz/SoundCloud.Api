using System;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Base
{
    public abstract class Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("id")]
        [JsonIgnoreOnSerialize]
        public long Id { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("kind")]
        [JsonIgnoreOnSerialize]
        public Kind Kind { get; set; }

        internal virtual BoxedEntity ToBoxedEntity()
        {
            throw new NotSupportedException("BoxedEntity not available for entity of type " + GetType());
        }
    }
}
