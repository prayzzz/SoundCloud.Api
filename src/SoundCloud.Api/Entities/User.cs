using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
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
            subscriptions = new List<Products>();
            Kind = Kind.User;
        }

        /// <summary>
        ///     Available for GET requests
        ///     URL to a JPEG image
        /// </summary>
        public string avatar_url { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     city
        /// </summary>
        public string city { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     country
        /// </summary>
        public string country { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     Discogs name
        /// </summary>
        public string discogs_name { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     first name
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of followers
        /// </summary>
        public int followers_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of followed users
        /// </summary>
        public int followings_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     first and last name
        /// </summary>
        public string full_name { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public string last_modified { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     last name
        /// </summary>
        public string last_name { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     MySpace name
        /// </summary>
        public string myspace_name { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     online status
        /// </summary>
        public bool online { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     permalink of the resource
        /// </summary>
        public string permalink { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     URL to the SoundCloud.com page
        /// </summary>
        public string permalink_url { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public string plan { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of public playlists
        /// </summary>
        [JsonProperty("playlist_count", NullValueHandling = NullValueHandling.Ignore)]
        public int playlist_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public bool primary_email_confirmed { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public int private_playlists_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public int private_tracks_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of favorited public tracks
        /// </summary>
        public int public_favorites_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public List<Products> subscriptions { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     number of public tracks
        /// </summary>
        public int track_count { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        public Uri uri { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     username
        /// </summary>
        public string username { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     a URL to the website
        /// </summary>
        public string website { get; set; }

        /// <summary>
        ///     Available for GET requests
        ///     a custom title for the website
        /// </summary>
        public string website_title { get; set; }

        public bool ValidateFollowUnfollow(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("UserId missing. Use the id property to set the id of this user.");
                return false;
            }

            return true;
        }

        public bool ValidateGet(ValidationMessages messages)
        {
            if (Id < 1)
            {
                messages.Add("UserId missing. Use the id property to set the id of this user.");
                return false;
            }

            return true;
        }
    }
}