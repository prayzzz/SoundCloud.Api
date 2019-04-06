using System.Collections.Generic;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a status response
    /// </summary>
    public sealed class StatusResponse
    {
        public StatusResponse()
        {
            Errors = new List<ErrorMessage>();
        }

        [JsonIgnoreOnSerialize]
        [JsonProperty("error", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonIgnoreOnSerialize]
        [JsonProperty("errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<ErrorMessage> Errors { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     Used for Resolve.GetUrl
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("location", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonIgnoreOnSerialize]
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Status { get; set; }
    }
}