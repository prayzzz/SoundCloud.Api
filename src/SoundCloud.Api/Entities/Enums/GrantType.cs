using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum GrantType
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "authorization_code")]
        AuthorizationCode,

        [EnumMember(Value = "refresh_token")]
        RefreshToken,

        [EnumMember(Value = "password")]
        Password,

        [EnumMember(Value = "client_credentials")]
        ClientCredentials,

        [EnumMember(Value = "oauth1_token")]
        OAuth1Token
    }
}