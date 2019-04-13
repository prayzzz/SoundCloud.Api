using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Kind
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "user")]
        User,

        [EnumMember(Value = "comment")]
        Comment,

        [EnumMember(Value = "connection")]
        Connection,

        [EnumMember(Value = "track")]
        Track,

        [EnumMember(Value = "secret-token")]
        SecretToken,

        [EnumMember(Value = "playlist")]
        Playlist,

        [EnumMember(Value = "app")]
        App,

        [EnumMember(Value = "web-profile")]
        WebProfile,

        [EnumMember(Value = "status")]
        Status
    }
}