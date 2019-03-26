using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EncodingStateEnum
    {
        [EnumMember(Value = "processing")]
        Processing,

        [EnumMember(Value = "failed")]
        Failed,

        [EnumMember(Value = "finished")]
        Finished,

        [EnumMember(Value = "storing")]
        Storing
    }
}