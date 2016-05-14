// ReSharper disable InconsistentNaming

using System;

using Newtonsoft.Json;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a comment
    /// </summary>
    public sealed class Comment : Entity
    {
        /// <summary>
        /// Available for GET, PUT, POST requests
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime created_at { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// </summary>
        public int? timestamp { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public int? track_id { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri uri { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public User user { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int? user_id { get; set; }

        public bool ValidateDelete(ValidationMessages messages)
        {
            if (track_id < 1)
            {
                messages.Add("TrackId missing. Use the track_id property to set the TrackId of this comment.");
                return false;
            }

            return true;
        }

        internal override void AppendCredentialsToProperties(SoundCloudCredentials credentials)
        {
            user?.AppendCredentialsToProperties(credentials);
            uri = uri.AppendCredentials(credentials);
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new CommentsBox(this);
        }

        internal bool ValidatePost(ValidationMessages messages)
        {
            if (track_id < 1)
            {
                messages.Add("TrackId missing. Use the track_id property to set the TrackId of this comment.");
                return false;
            }

            if (string.IsNullOrEmpty(body))
            {
                messages.Add("Message missing. Use the body property to set your message.");
                return false;
            }

            return true;
        }

        internal sealed class CommentsBox : BoxedEntity
        {
            public CommentsBox(Comment c)
            {
                comment = c;
            }

            public Comment comment { get; set; }
        }
    }
}