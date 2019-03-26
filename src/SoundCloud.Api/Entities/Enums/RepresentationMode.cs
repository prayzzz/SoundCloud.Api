using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum RepresentationMode
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "compact")]
        Compact,

        [EnumMember(Value = "id")]
        Id
    }
}