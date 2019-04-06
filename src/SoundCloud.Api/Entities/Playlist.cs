using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a playlist
    /// </summary>
    public sealed class Playlist : Entity
    {
        /// <summary>
        ///     Constructs a new <see cref="Playlist" /> object.
        /// </summary>
        public Playlist()
        {
            Tracks = new List<Track>();
            TagList = new List<string>();
        }

        /// <summary>
        ///     Available for GET requests
        ///     URL to a JPEG image
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("artwork_url")]
        public string ArtworkUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     timestamp of creation
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("created_with")]
        public AppClient CreatedWith { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     HTML description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     downloadable
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("downloadable")]
        public bool? Downloadable { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     duration in milliseconds
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     EAN identifier for the playlist
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("ean")]
        public string Ean { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     who can embed this track or playlist
        /// </summary>
        [JsonProperty("embeddable_by")]
        public EmbeddableBy EmbeddableBy { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     genre
        /// </summary>
        [JsonProperty("genre")]
        public string Genre { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     label mini user object
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("label")]
        public User Label { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     id of the label user
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("label_id")]
        public string LabelId { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     label name
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("label_name")]
        public string LabelName { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     timestamp of last modification
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("last_modified")]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime LastModified { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("license")]
        public License License { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     permalink of the resource
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     URL to the SoundCloud.com page
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     playlist type
        /// </summary>
        [JsonProperty("playlist_type")]
        public PlaylistType PlaylistType { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("purchase_title")]
        public string PurchaseTitle { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     external purchase link
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("purchase_url")]
        public string PurchaseUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     release number
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("release")]
        public string Release { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     day of the release
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("release_day")]
        public string ReleaseDay { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     month of the release
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("release_month")]
        public string ReleaseMonth { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     year of the release
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("release_year")]
        public string ReleaseYear { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("secret_token")]
        public string SecretToken { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("secret_uri")]
        public string SecretUri { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     public/private sharing
        /// </summary>
        [JsonProperty("sharing")]
        public Sharing Sharing { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     streamable via API
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("streamable")]
        public bool? Streamable { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     list of tags
        /// </summary>
        [JsonProperty("tag_list")]
        [JsonConverter(typeof(StringToListJsonConverter), ' ', '"')]
        public List<string> TagList { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     track title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("track_count")]
        public int TrackCount { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        /// </summary>
        [JsonProperty("tracks")]
        [JsonConverter(typeof(PlaylistTracksJsonConverter))]
        public List<Track> Tracks { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("type")]
        [JsonIgnoreOnSerialize]
        public PlaylistType Type { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     API resource URL
        /// </summary>
        [JsonProperty("uri")]
        [JsonIgnoreOnSerialize]
        public Uri Uri { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     mini user representation of the owner
        /// </summary>
        [JsonProperty("user")]
        [JsonIgnoreOnSerialize]
        public User User { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     user-id of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        public void ValidateDelete()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidateGet()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
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
                messages.Add("Title missing. Use the title property to set your track title.");
            }

            if (PlaylistType == PlaylistType.Other)
            {
                messages.Add("Playlist type must not be 'other'.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidateUpdate()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
            }

            if (string.IsNullOrEmpty(Title))
            {
                messages.Add("Title missing. Use the title property to set your track title.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidateUploadArtwork()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("PlaylistId missing. Use the id property to set the id of this playlist.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new PlaylistBox(this);
        }

        private sealed class PlaylistBox : BoxedEntity
        {
            public PlaylistBox(Playlist p)
            {
                Playlist = p;
            }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            // ReSharper disable once MemberCanBePrivate.Local
            [JsonProperty("playlist")]
            public Playlist Playlist { get; }
        }
    }
}