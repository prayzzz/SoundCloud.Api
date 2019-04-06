using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a user
    /// </summary>
    public sealed class User : Entity
    {
        /// <summary>
        ///     Constructs a new <see cref="User" /> object
        /// </summary>
        public User()
        {
            Subscriptions = new List<Products>();
            Kind = Kind.User;
        }

        /// <summary>
        ///     Available for GET requests
        ///     URL to a JPEG image
        /// </summary>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     city
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     country
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     Discogs name
        /// </summary>
        [JsonProperty("discogs_name")]
        public string DiscogsName { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     first name
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of followers
        /// </summary>
        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of followed users
        /// </summary>
        [JsonProperty("followings_count")]
        public int FollowingsCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     first and last name
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("last_modified")]
        public string LastModified { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     last name
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     MySpace name
        /// </summary>
        [JsonProperty("myspace_name")]
        public string MyspaceName { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     online status
        /// </summary>
        [JsonProperty("online")]
        public bool Online { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     permalink of the resource
        /// </summary>
        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     URL to the SoundCloud.com page
        /// </summary>
        [JsonProperty("permalink_url")]
        public string PermalinkUrl { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("plan")]
        public string Plan { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of public playlists
        /// </summary>
        [JsonProperty("playlist_count", NullValueHandling = NullValueHandling.Ignore)]
        public int PlaylistCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("primary_email_confirmed")]
        public bool PrimaryEmailConfirmed { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("private_playlists_count")]
        public int PrivatePlaylistsCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("private_tracks_count")]
        public int PrivateTracksCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of favorited public tracks
        /// </summary>
        [JsonProperty("public_favorites_count")]
        public int PublicFavoritesCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("subscriptions")]
        public List<Products> Subscriptions { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of public tracks
        /// </summary>
        [JsonProperty("track_count")]
        public int TrackCount { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     username
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     a URL to the website
        /// </summary>
        [JsonProperty("website")]
        public string Website { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     a custom title for the website
        /// </summary>
        [JsonProperty("website_title")]
        public string WebsiteTitle { get; set; }

        public void ValidateFollowUnfollow()
        {
            var messages = new ValidationMessages();

            if (Id < 1)
            {
                messages.Add("UserId missing. Use the id property to set the id of this user.");
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
                messages.Add("UserId missing. Use the id property to set the id of this user.");
            }

            if (messages.HasErrors)
            {
                throw new SoundCloudValidationException(messages);
            }
        }
    }
}