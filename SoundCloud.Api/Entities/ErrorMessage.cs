// ReSharper disable InconsistentNaming

using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents an error message
    /// </summary>
    public class ErrorMessage
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string error_message { get; set; }
    }
}