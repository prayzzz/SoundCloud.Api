using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents an error message
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("error_message")]
        public string Message { get; set; }
    }
}