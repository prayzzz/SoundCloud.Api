using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.QueryBuilders
{
    public class TrackQueryBuilder : SoundCloudQueryBuilder
    {
        public TrackQueryBuilder()
        {
            SearchString = string.Empty;
            Sharing = Sharing.None;
            License = License.None;
            BpmFrom = null;
            BpmTo = null;
            DurationFrom = null;
            DurationTo = null;
            CreatedAtFrom = null;
            CreatedAtTo = null;
            Ids = new List<int>();
            Genres = new List<string>();
            TrackTypes = new List<TrackType>();
            Tags = new List<string>();
        }

        public int? BpmFrom { get; set; }

        public int? BpmTo { get; set; }

        public DateTime? CreatedAtFrom { get; set; }

        public DateTime? CreatedAtTo { get; set; }

        public int? DurationFrom { get; set; }

        public int? DurationTo { get; set; }

        public List<string> Genres { get; }

        public List<int> Ids { get; }

        public License License { get; set; }

        public string SearchString { get; set; }

        public Sharing Sharing { get; set; }

        public List<string> Tags { get; }

        public List<TrackType> TrackTypes { get; }

        protected override void AddArguments(IDictionary<string, string> queryArguments)
        {
            base.AddArguments(queryArguments);

            if (!string.IsNullOrEmpty(SearchString))
            {
                ApplyPrimitiveType(queryArguments, "q", SearchString);
            }

            ApplyNullablePrimitiveType(queryArguments, "bpm[from]", BpmFrom, s => s.ToString());
            ApplyNullablePrimitiveType(queryArguments, "bpm[to]", BpmTo, s => s.ToString());
            ApplyNullablePrimitiveType(queryArguments, "duration[from]", DurationFrom, s => s.ToString());
            ApplyNullablePrimitiveType(queryArguments, "duration[to]", DurationTo, s => s.ToString());

            ApplyNullableEnumType(queryArguments, "license", License, License.None, s => s.GetAttributeOfType<EnumMemberAttribute>().Value);
            ApplyNullableEnumType(queryArguments, "filter", Sharing, Sharing.None, s => s.GetAttributeOfType<EnumMemberAttribute>().Value);

            ApplyNullableDateTimeType(queryArguments, "created_at[from]", CreatedAtFrom, s => s.ToString(Settings.SoundCloudDateTimeQueryPattern));
            ApplyNullableDateTimeType(queryArguments, "created_at[to]", CreatedAtTo, s => s.ToString(Settings.SoundCloudDateTimeQueryPattern));

            ApplyList(queryArguments, "types", TrackTypes, t => t.GetAttributeOfType<EnumMemberAttribute>().Value);
            ApplyList(queryArguments, "tags", Tags);
            ApplyList(queryArguments, "ids", Ids);
            ApplyList(queryArguments, "genres", Genres);
        }
    }
}