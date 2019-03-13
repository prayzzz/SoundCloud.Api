// ReSharper disable InconsistentNaming

using System;

using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities.Base
{
    public abstract class Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int id { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Kind kind { get; set; }

        internal abstract void AppendCredentialsToProperties(SoundCloudCredentials credentials);

        internal virtual BoxedEntity ToBoxedEntity()
        {
            throw new NotSupportedException("BoxedEntity not available for entity of type " + GetType());
        }
    }
}