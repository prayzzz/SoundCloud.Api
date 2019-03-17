// ReSharper disable InconsistentNaming

using System;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a group
    /// </summary>
    public sealed class Group : Entity
    {
        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string artwork_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int contributors_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string created_at { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public User creator { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int members_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public bool moderated { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string permalink { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string permalink_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public string short_description { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int track_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri uri { get; set; }

        public bool ValidateDelete(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("GroupId missing. Use the id property to set the id of this group.");
                return false;
            }

            return true;
        }

        public bool ValidateGet(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("GroupId missing. Use the id property to set the id of this group.");
                return false;
            }

            return true;
        }

        public bool ValidatePost(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(name))
            {
                messages.Add("Name missing. Use the name property to set your group name.");
                return false;
            }

            return true;
        }

        public bool ValidateUpdate(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("GroupId missing. Use the id property to set the id of this group.");
                return false;
            }

            if (string.IsNullOrEmpty(name))
            {
                messages.Add("Name missing. Use the name property to set your group name.");
                return false;
            }

            return true;
        }

        public bool ValidateUploadArtwork(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("GroupId missing. Use the id property to set the id of this group.");
                return false;
            }

            return true;
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new GroupBox(this);
        }

        private sealed class GroupBox : BoxedEntity
        {
            public GroupBox(Group g)
            {
                group = g;
            }

            public Group group { get; set; }
        }
    }
}