using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a web profile
    /// </summary>
    public sealed class WebProfile : Entity
    {
        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     Available for GET, POST requests
        /// </summary>
        [JsonProperty("network")]
        public WebService Network { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("service")]
        public WebService Service
        {
            get => Network;
            set => Network = value;
        }

        /// <summary>
        ///     Available for GET, POST requests
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Available for GET, POST requests
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("username")]
        public string Username { get; set; }

        public void ValidateDelete()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("WebProfile id missing. Use the id property to set the id of this WebProfile.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidatePost()
        {
            var messages = new ValidationMessages();

            if (string.IsNullOrEmpty(Title))
            {
                messages.Add("WebProfile title missing. Use the title property to set the title of this WebProfile.");
            }

            if (string.IsNullOrEmpty(Url))
            {
                messages.Add("WebProfile url missing. Use the url property to set the url of this WebProfile.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new WebProfileBox(this);
        }

        private sealed class WebProfileBox : BoxedEntity
        {
            public WebProfileBox(WebProfile wb)
            {
                WebProfile = wb;
            }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            // ReSharper disable once MemberCanBePrivate.Local
            [JsonProperty("web_profile")]
            public WebProfile WebProfile { get; }
        }
    }
}