using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum EmbeddableBy
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "all")]
        All,

        [EnumMember(Value = "me")]
        Me
    }
}