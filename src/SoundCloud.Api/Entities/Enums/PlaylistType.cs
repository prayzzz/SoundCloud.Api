using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum PlaylistType
    {
        [EnumMember(Value = "other")]
        Other = 0,

        // ReSharper disable once InconsistentNaming
        [EnumMember(Value = "ep single")]
        EPSingle,

        [EnumMember(Value = "album")]
        Album,

        [EnumMember(Value = "compilation")]
        Compilation,

        [EnumMember(Value = "project files")]
        ProjectFiles,

        [EnumMember(Value = "archive")]
        Archive,

        [EnumMember(Value = "showcase")]
        Showcase,

        [EnumMember(Value = "demo")]
        Demo,

        [EnumMember(Value = "sample pack")]
        SamplePack
    }
}