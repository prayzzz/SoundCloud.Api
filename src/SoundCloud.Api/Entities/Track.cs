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
    /// Represents a track
    /// </summary>
    public sealed class Track : Entity
    {
        /// <summary>
        /// Constructs a new <see cref="Track"/> object
        /// </summary>
        public Track()
        {
            tag_list = new List<string>();
        }

        /// <summary>
        /// Available for GET requests
        /// URL to a JPEG image
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri artwork_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri attachments_uri { get; set; }

        /// <summary>
        /// Available for GET requests
        /// beats per minute
        /// </summary>
        [JsonIgnoreOnSerialize]
        public double? bpm { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track comment count
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int comment_count { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// track commentable, only for Pro users
        /// Default value is true
        /// </summary>
        public bool commentable { get; set; }

        /// <summary>
        /// Available for GET requests
        /// timestamp of creation
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime created_at { get; set; }

        /// <summary>
        /// Available for GET requests
        /// the app that the track created
        /// </summary>
        [JsonIgnoreOnSerialize]
        public AppClient created_with { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// HTML description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime disabled_at { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string disabled_reason { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track download count
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int download_count { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// URL to original file
        /// </summary>
        public Uri download_url { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// downloadable 
        /// Default value is false
        /// </summary>
        public bool? downloadable { get; set; }

        /// <summary>
        /// Available for GET requests
        /// duration in milliseconds
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int duration { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? embeddable { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// who can embed this track or playlist
        /// Default value is <see cref="EmbeddableBy.All"/>
        /// </summary>
        public EmbeddableBy embeddable_by { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track favoriting count
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int favoritings_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? feedable { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// genre
        /// </summary>
        public string genre { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? geo_blocking { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? has_downloads_left { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track ISRC
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string isrc { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track key
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string key_signature { get; set; }

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
        public int? label_id { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// label name
        /// </summary>]
        public string label_name { get; set; }

        /// <summary>
        /// Available for GET requests
        /// timestamp of last modification
        /// </summary>
        [JsonIgnoreOnSerialize]
        [JsonConverter(typeof(DateTimeConverter), Settings.SoundCloudDateTimeWithTimezonePattern)]
        public DateTime last_modified { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// creative common license
        /// Default value is <see cref="Enums.License.AllRightsReserved"/>
        /// </summary>
        public License license { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track repost count
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int likes_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string monetization_model { get; set; }

        /// <summary>
        /// Available for GET requests
        /// size in bytes of the original file
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int? original_content_size { get; set; }

        /// <summary>
        /// Available for GET requests
        /// file format of the original file
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string original_format { get; set; }

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
        public Uri permalink_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track play count
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int playback_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string policy { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// external purchase link
        /// </summary>
        public string purchase_title { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// external purchase link
        /// </summary>
        public Uri purchase_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// release number
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string release { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// day of the release
        /// </summary>
        public int? release_day { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// month of the release
        /// </summary>
        public int? release_month { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// year of the release
        /// </summary>
        public int? release_year { get; set; }

        /// <summary>
        /// Available for GET requests
        /// track repost count
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int reposts_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? reveal_comments { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? reveal_stats { get; set; }

        /// <summary>
        /// Available for GET, PUT, POST requests
        /// public/private sharing
        /// Default value is <see cref="Sharing.Private"/>
        /// </summary>
        public Sharing sharing { get; set; }

        /// <summary>
        /// Available for GET requests
        /// encoding state
        /// </summary>
        [JsonIgnoreOnSerialize]
        public EncodingStateEnum state { get; set; }

        /// <summary>
        /// Available for GET requests
        /// link to 128kbs mp3 stream
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri stream_url { get; set; }

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
        /// Available for GET, PUT, POST requests
        /// track type
        /// Default value is <see cref="TrackType.None"/>
        /// </summary>
        public TrackType track_type { get; set; }

        /// <summary>
        /// Available for GET requests
        /// API resource URL
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri uri { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string urn { get; set; }

        /// <summary>
        /// Available for GET requests
        /// mini user representation of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        public User user { get; set; }

        /// <summary>
        /// Available for GET requests (authenticated)
        /// track favorite of current user
        /// </summary>
        [JsonIgnoreOnSerialize]
        public bool? user_favorite { get; set; }

        /// <summary>
        /// Available for GET requests
        /// user-id of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int user_id { get; set; }

        /// <summary>
        /// Available for GET requests (authenticated)
        /// track play count of current user
        /// </summary>
        [JsonIgnoreOnSerialize]
        public int? user_playback_count { get; set; }

        /// <summary>
        /// Available for GET requests
        /// user-uri of the owner
        /// </summary>
        [JsonIgnoreOnSerialize]
        public Uri user_uri { get; set; }

        /// <summary>
        /// Available for GET requests
        /// a link to a video page
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string video_url { get; set; }

        /// <summary>
        /// Available for GET requests
        /// URL to PNG waveform image
        /// </summary>
        [JsonIgnoreOnSerialize]
        public string waveform_url { get; set; }

        public bool ValidateDelete(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
                return false;
            }

            return true;
        }

        public bool ValidateGet(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
                return false;
            }

            return true;
        }

        public bool ValidateLikeUnlike(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
                return false;
            }

            return true;
        }

        public bool ValidateUpdate(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
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
                messages.Add("TrackId missing. Use the id property to set the id of this track.");
                return false;
            }

            return true;
        }

        internal override BoxedEntity ToBoxedEntity()
        {
            return new TrackBox(this);
        }

        private sealed class TrackBox : BoxedEntity
        {
            public TrackBox(Track t)
            {
                track = t;
            }

            public Track track { get; set; }
        }
    }
}