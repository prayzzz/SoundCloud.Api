// ReSharper disable InconsistentNaming

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public sealed class Products : Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string name { get; set; }
    }
}