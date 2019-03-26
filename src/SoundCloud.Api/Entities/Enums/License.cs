using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum License
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "no-rights-reserved")]
        NoRightsReserved,

        [EnumMember(Value = "all-rights-reserved")]
        AllRightsReserved,

        [EnumMember(Value = "cc-by")]
        CcBy,

        [EnumMember(Value = "cc-by-nc")]
        CcByNc,

        [EnumMember(Value = "cc-by-nd")]
        CcByNd,

        [EnumMember(Value = "cc-by-sa")]
        CcBySa,

        [EnumMember(Value = "cc-by-nc-nd")]
        CcByNcNd,

        [EnumMember(Value = "cc-by-nc-sa")]
        CcByNcSa
    }
}