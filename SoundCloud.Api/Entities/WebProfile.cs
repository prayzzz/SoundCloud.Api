// ReSharper disable InconsistentNaming

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a web profile
    /// </summary>
    public sealed class WebProfile : Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string created_at { get; set; }

        /// <summary>
        /// Available for GET, POST requests
        /// </summary>
        public WebService network { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public WebService service
        {
            get { return network; }
            set { network = value; }
        }

        /// <summary>
        /// Available for GET, POST requests
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Available for GET, POST requests
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string username { get; set; }

        public bool ValidateDelete(ValidationMessages messages)
        {
            if (id < 1)
            {
                messages.Add("WebProfile id missing. Use the id property to set the id of this WebProfile.");
                return false;
            }

            return true;
        }

        public bool ValidatePost(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(title))
            {
                messages.Add("WebProfile title missing. Use the title property to set the title of this WebProfile.");
                return false;
            }

            if (string.IsNullOrEmpty(url))
            {
                messages.Add("WebProfile url missing. Use the url property to set the url of this WebProfile.");
                return false;
            }

            return true;
        }

        internal override void AppendCredentialsToProperties(SoundCloudCredentials credentials)
        {
        }

        internal override BoxedEntity ToBoxedEntity => new WebProfileBox(this);

        private sealed class WebProfileBox : BoxedEntity
        {
            public WebProfileBox(WebProfile wb)
            {
                web_profile = wb;
            }

            public WebProfile web_profile { get; set; }
        }
    }
}