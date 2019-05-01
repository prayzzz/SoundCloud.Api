using System.Collections.Generic;
using System.Runtime.Serialization;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.QueryBuilders
{
    public class PlaylistQueryBuilder : SoundCloudQueryBuilder
    {
        /// <summary>
        ///     Builds a new instance of <see cref="PlaylistQueryBuilder" />.
        ///     Number of results is limited to 50.
        /// </summary>
        public PlaylistQueryBuilder()
        {
            CustomMaxLimit = 50;
            SearchString = "";
            Representation = RepresentationMode.None;
        }

        public RepresentationMode Representation { get; set; }

        public string SearchString { get; set; }

        protected override void AddArguments(IDictionary<string, string> queryArguments)
        {
            base.AddArguments(queryArguments);

            ApplyPrimitiveType(queryArguments, "q", SearchString);
            ApplyNullableEnumType(queryArguments,
                "representation",
                Representation,
                RepresentationMode.None,
                s => s.GetAttributeOfType<EnumMemberAttribute>().Value);
        }
    }
}
