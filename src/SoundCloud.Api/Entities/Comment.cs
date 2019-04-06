using System;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a comment
    /// </summary>
    public sealed class Comment : Entity
    {
        /// <summary>
        ///     Available for GET, PUT, POST requests
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        /// </summary>
        [JsonProperty("timestamp")]
        public int? Timestamp { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("track_id")]
        public int? TrackId { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("uri")]
        [JsonIgnoreOnSerialize]
        public Uri Uri { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("user")]
        [JsonIgnoreOnSerialize]
        public User User { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("user_id")]
        public int? UserId { get; set; }

        public void ValidateDelete()
        {
            var messages = new ValidationMessages();

            if (TrackId < 1)
            {
                messages.Add("TrackId missing. Use the track_id property to set the TrackId of this comment.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        internal void ValidatePost()
        {
            var messages = new ValidationMessages();

            if (TrackId < 1)
            {
                messages.Add("TrackId missing. Use the track_id property to set the TrackId of this comment.");
            }

            if (string.IsNullOrEmpty(Body))
            {
                messages.Add("Message missing. Use the body property to set your message.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new CommentsBox(this);
        }

        internal sealed class CommentsBox : BoxedEntity
        {
            public CommentsBox(Comment c)
            {
                Comment = c;
            }

            [JsonProperty("comment")]
            public Comment Comment { get; set; }
        }
    }
}