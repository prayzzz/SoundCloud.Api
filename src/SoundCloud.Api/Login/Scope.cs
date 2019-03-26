using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Login
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum Scope
    {
        [EnumMember(Value = "")]
        None,

        [EnumMember(Value = "non-expiring")]
        NonExpiring
    }
}