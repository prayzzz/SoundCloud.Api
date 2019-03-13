// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    /// Represents a list with a link to the previous and next page
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class PagedResult<TEntity> : IPagedResult<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Constructs a new <see cref="PagedResult{TEntity}"/> object
        /// </summary>
        public PagedResult()
        {
            collection = new List<TEntity>();
        }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public Uri future_href { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public List<TEntity> collection { get; set; }

        /// <summary>
        /// Available for GET requests
        /// </summary>
        [JsonIgnore]
        public bool HasNextPage => next_href != null && !string.IsNullOrEmpty(next_href.ToString());

        /// <summary>
        /// Available for GET requests
        /// </summary>
        public Uri next_href { get; set; }

        internal void AppendCredentialsToProperties(SoundCloudCredentials credentials)
        {
            future_href = future_href.AppendCredentials(credentials);
            next_href = next_href.AppendCredentials(credentials);
        }
    }
}