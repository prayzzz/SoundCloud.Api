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
    ///     Represents a track
    /// </summary>
    public sealed class Track : Entity
    {
        /// <summary>
        ///     Constructs a new <see cref="Track" /> object
        /// </summary>
        public Track()
        {
            TagList = new List<string>();
        }

        /// <summary>
        ///     Available for GET requests
        ///     URL to a JPEG image
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("artwork_url")]
        public Uri ArtworkUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("attachments_uri")]
        public Uri AttachmentsUri { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     beats per minute
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("bpm")]
        public double? Bpm { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track comment count
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     track commentable, only for Pro users
        ///     Default value is true
        /// </summary>
        [JsonProperty("commentable")]
        public bool Commentable { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     timestamp of creation
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("created_at")]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     the app that the track created
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
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("disabled_at")]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime DisabledAt { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("disabled_reason")]
        public string DisabledReason { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track download count
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("download_count")]
        public int DownloadCount { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     URL to original file
        /// </summary>
        [JsonProperty("download_url")]
        public Uri DownloadUrl { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     downloadable
        ///     Default value is false
        /// </summary>
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
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("embeddable")]
        public bool? Embeddable { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     who can embed this track or playlist
        ///     Default value is <see cref="Enums.EmbeddableBy.All" />
        /// </summary>
        [JsonProperty("embeddable_by")]
        public EmbeddableBy EmbeddableBy { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track favoritings count
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("favoritings_count")]
        public int FavoritingsCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("feedable")]
        public bool? Feedable { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     genre
        /// </summary>
        [JsonProperty("genre")]
        public string Genre { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("geo_blocking")]
        public bool? GeoBlocking { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("has_downloads_left")]
        public bool? HasDownloadsLeft { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track ISRC
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("isrc")]
        public string Isrc { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track key
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("key_signature")]
        public string KeySignature { get; set; }

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
        public int? LabelId { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     label name
        /// </summary>
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
        ///     Available for GET, PUT, POST requests
        ///     creative common license
        ///     Default value is <see cref="Enums.License.AllRightsReserved" />
        /// </summary>
        [JsonProperty("license")]
        public License License { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track repost count
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("likes_count")]
        public int LikesCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("monetization_model")]
        public string MonetizationModel { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     size in bytes of the original file
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("original_content_size")]
        public int? OriginalContentSize { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     file format of the original file
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("original_format")]
        public string OriginalFormat { get; set; }

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
        public Uri PermalinkUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track play count
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("playback_count")]
        public int PlaybackCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("policy")]
        public string Policy { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     external purchase link
        /// </summary>
        [JsonProperty("purchase_title")]
        public string PurchaseTitle { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     external purchase link
        /// </summary>
        [JsonProperty("purchase_url")]
        public Uri PurchaseUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     release number
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("release")]
        public string Release { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     day of the release
        /// </summary>
        [JsonProperty("release_day")]
        public int? ReleaseDay { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     month of the release
        /// </summary>
        [JsonProperty("release_month")]
        public int? ReleaseMonth { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     year of the release
        /// </summary>
        [JsonProperty("release_year")]
        public int? ReleaseYear { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     track repost count
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("reposts_count")]
        public int RepostsCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("reveal_comments")]
        public bool? RevealComments { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("reveal_stats")]
        public bool? RevealStats { get; set; }

        /// <summary>
        ///     Available for GET, PUT, POST requests
        ///     public/private sharing
        ///     Default value is <see cref="Enums.Sharing.Private" />
        /// </summary>
        [JsonProperty("sharing")]
        public Sharing Sharing { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     encoding state
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("state")]
        public EncodingStateEnum State { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     link to 128kbs mp3 stream
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("stream_url")]
        public Uri StreamUrl { get; set; }

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
        ///     Available for GET, PUT, POST requests
        ///     track type
        ///     Default value is <see cref="Enums.TrackType.None" />
        /// </summary>
        [JsonProperty("track_type")]
        public TrackType TrackType { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     API resource URL
        /// </summary>
        [JsonProperty("uri")]
        [JsonIgnoreOnSerialize]
        public Uri Uri { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("urn")]
        [JsonIgnoreOnSerialize]
        public string Urn { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     mini user representation of the owner
        /// </summary>
        [JsonProperty("user")]
        [JsonIgnoreOnSerialize]
        public User User { get; set; }

        /// <summary>
        ///     Available for GET requests (authenticated)
        ///     track favorite of current user
        /// </summary>
        [JsonProperty("user_favorite")]
        [JsonIgnoreOnSerialize]
        public bool? UserFavorite { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     user-id of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        /// <summary>
        ///     Available for GET requests (authenticated)
        ///     track play count of current user
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("user_playback_count")]
        public int? UserPlaybackCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     user-uri of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("user_uri")]
        public Uri UserUri { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     a link to a video page
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     URL to PNG waveform image
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonProperty("waveform_url")]
        public string WaveformUrl { get; set; }

        public void ValidateDelete()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
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
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        public void ValidateLikeUnlike()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
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
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
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
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new TrackBox(this);
        }

        private sealed class TrackBox : BoxedEntity
        {
            public TrackBox(Track t)
            {
                Track = t;
            }

            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            // ReSharper disable once MemberCanBePrivate.Local
            [JsonProperty("track")]
            public Track Track { get; }
        }
    }
}