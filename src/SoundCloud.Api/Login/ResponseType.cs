using System.Runtime.Serialization;

namespace SoundCloud.Api.Login
{
    public enum ResponseType
    {
        [EnumMember(Value = "code")]
        Code,

        [EnumMember(Value = "token")]
        Token
    }
}