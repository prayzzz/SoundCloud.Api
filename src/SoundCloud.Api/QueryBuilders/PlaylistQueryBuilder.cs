using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.QueryBuilders
{
    public class PlaylistQueryBuilder : SoundCloudQueryBuilder
    {
        /// <summary>
        ///     Builds a new instance of <see cref="PlaylistQueryBuilder" />
        ///     Number of results is limited to 10
        /// </summary>
        /// <param name="search">The search string should not be empty.</param>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="search" /> is null or empty.</exception>
        public PlaylistQueryBuilder(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new ArgumentException("SearchString should not be empty.", SearchString);
            }

            CustomMaxLimit = 10;
            SearchString = search;
            Representation = RepresentationMode.None;
        }

        internal PlaylistQueryBuilder()
        {
        }

        public RepresentationMode Representation { get; set; }

        public string SearchString { get; set; }

        protected override void AddArguments(IDictionary<string, string> queryArguments)
        {
            base.AddArguments(queryArguments);

            if (Paged && string.IsNullOrEmpty(SearchString))
            {
                throw new ArgumentException("SearchString should not be empty.", SearchString);
            }

            ApplyPrimitiveType(queryArguments, "q", SearchString);
            ApplyNullableEnumType(queryArguments, "representation", Representation, RepresentationMode.None,
                s => s.GetAttributeOfType<EnumMemberAttribute>().Value);
        }
    }
}