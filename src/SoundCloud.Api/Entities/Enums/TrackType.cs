using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum TrackType
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "original")]
        Original,

        [EnumMember(Value = "remix")]
        Remix,

        [EnumMember(Value = "live")]
        Live,

        [EnumMember(Value = "recording")]
        Recording,

        [EnumMember(Value = "spoken")]
        Spoken,

        [EnumMember(Value = "podcast")]
        Podcast,

        [EnumMember(Value = "demo")]
        Demo,

        [EnumMember(Value = "in progress")]
        InProgress,

        [EnumMember(Value = "stem")]
        Stem,

        [EnumMember(Value = "loop")]
        Loop,

        [EnumMember(Value = "sound effect")]
        SoundEffect,

        [EnumMember(Value = "sample")]
        Sample,

        [EnumMember(Value = "other")]
        Other
    }
}