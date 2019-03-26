using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityType
    {
        [EnumMember(Value = "other")]
        Other = 0,

        [EnumMember(Value = "track-repost")]
        TrackRepost,

        [EnumMember(Value = "playlist-repost")]
        PlaylistRepost,

        [EnumMember(Value = "playlist")]
        Playlist,

        [EnumMember(Value = "track")]
        Track,

        [EnumMember(Value = "comment")]
        Comment,

        [EnumMember(Value = "favoriting")]
        Favoriting
    }
}