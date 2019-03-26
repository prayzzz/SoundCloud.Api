using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a product
    /// </summary>
    public sealed class Products : Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("name")]
        [JsonIgnoreOnSerialize]
        public string Name { get; set; }
    }
}