using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Sharing
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "all")]
        All,

        [EnumMember(Value = "public")]
        Public,

        [EnumMember(Value = "private")]
        Private
    }
}