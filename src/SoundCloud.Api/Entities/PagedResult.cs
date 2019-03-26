using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SoundCloud.Api.Entities.Base;

namespace SoundCloud.Api.Entities
{
    /// <summary>
    ///     Represents a list with a link to the previous and next page
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class PagedResult<TEntity> : IPagedResult<TEntity> where TEntity : Entity
    {
        /// <summary>
        ///     Constructs a new <see cref="PagedResult{TEntity}" /> object
        /// </summary>
        public PagedResult()
        {
            Collection = new List<TEntity>();
        }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("future_href")]
        public Uri FutureHref { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("collection")]
        public List<TEntity> Collection { get; set; }

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonIgnore]
        public bool HasNextPage => NextHref != null && !string.IsNullOrEmpty(NextHref.ToString());

        /// <summary>
        ///     Available for GET requests
        /// </summary>
        [JsonProperty("next_href")]
        public Uri NextHref { get; set; }
    }
}