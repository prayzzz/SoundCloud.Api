// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a playlist
    /// </summary>
    public sealed class Playlist : Entity
    {
        /// <summary>
        /// Constructs a new <see cref="Playlist"/> object.
        /// </summary>
        public Playlist()
        {
            tracks = new List<Track>();
            tag_list = new List<string>();
        }

        /// <summary>
        /// Available for GET requests
        /// URL to a JPEG image
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string artwork_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string created_at { get; set; }

        /// <summary>
        /// Available for GET requests
        /// timestamp of creation
        /// </summary>
        [JsonIgnoreOnSerialize]
        public AppClient created_with { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// HTML description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// downloadable 
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? downloadable { get; set; }

        /// <summary>
        /// Available for GET requests
        /// duration in milliseconds
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int duration { get; set; }

        /// <summary>
        /// Available for GET requests
        /// EAN identifier for the playlist
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string ean { get; set; }

        /// <summary>
        /// Available for GET requests
        /// who can embed this track or playlist
        /// </summary>
        public EmbeddableBy embeddable_by { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// genre
        /// </summary>
        public string genre { get; set; }

        /// <summary>
        /// Available for GET requests
        /// label mini user object
        /// </summary>
        [JsonIgnoreOnSerialize]
        public User label { get; set; }

        /// <summary>
        /// Available for GET requests
        /// id of the label user
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string label_id { get; set; }

        /// <summary>
        /// Available for GET requests
        /// label name
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string label_name { get; set; }

        /// <summary>
        /// Available for GET requests
        /// timestamp of last modification
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime last_modified { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public License license { get; set; }

        /// <summary>
        /// Available for GET requests
        /// permalink of the resource
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string permalink { get; set; }

        /// <summary>
        /// Available for GET requests
        /// URL to the SoundCloud.com page
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string permalink_url { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// playlist type
        /// </summary>
        public PlaylistType playlist_type { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string purchase_title { get; set; }

        /// <summary>
        /// Available for GET requests
        /// external purchase link
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string purchase_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// release number
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string release { get; set; }

        /// <summary>
        /// Available for GET requests
        /// day of the release
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string release_day { get; set; }

        /// <summary>
        /// Available for GET requests
        /// month of the release
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string release_month { get; set; }

        /// <summary>
        /// Available for GET requests
        /// year of the release
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string release_year { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public string secret_token { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public string secret_uri { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// public/private sharing
        /// </summary>
        public Sharing sharing { get; set; }

        /// <summary>
        /// Available for GET requests
        /// streamable via API 
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? streamable { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// list of tags
        /// </summary>
        [JsonConverter(typeof(StringToListJsonConverter), ' ', '"')]
        public List<string> tag_list { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// track title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int track_count { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// </summary>
        [JsonConverter(typeof(PlaylistTracksJsonConverter))]
        public List<Track> tracks { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public PlaylistType type { get; set; }

        /// <summary>
        /// Available for GET requests
        /// API resource URL
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri uri { get; set; }

        /// <summary>
        /// Available for GET requests
        /// mini user representation of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        public User user { get; set; }

        /// <summary>
        /// Available for GET requests
        /// user-id of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int user_id { get; set; }

        public bool ValidateDelete(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
                return false;
            }

            return true;
        }

        public bool ValidateGet(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
                return false;
            }

            return true;
        }

        public bool ValidatePost(ValidationMessages messages)
        {
            if (string.IsNullOrEmpty(title))
            {
                messages.Add("Title missing. Use the title property to set your track title.");
                return false;
            }

            if (playlist_type == PlaylistType.Other)
            {
                messages.Add("Playlist type must not be 'other'.");
                return false;
            }

            return true;
        }

        public bool ValidateUpdate(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
                return false;
            }

            if (string.IsNullOrEmpty(title))
            {
                messages.Add("Title missing. Use the title property to set your track title.");
                return false;
            }

            return true;
        }

        public bool ValidateUploadArtwork(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
                return false;
            }

            return true;
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new PlaylistBox(this);
        }

        private sealed class PlaylistBox : BoxedEntity
        {
            public PlaylistBox(Playlist p)
            {
                playlist = p;
            }

            public Playlist playlist { get; set; }
        }
    }
}