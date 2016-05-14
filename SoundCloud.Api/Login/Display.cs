using System.Runtime.Serialization;

namespace SoundCloud.Api.Login
{
    public enum Display
    {
        [EnumMember(Value = "popup")]
        Popup,

        [EnumMember(Value = "")]
        None
    }
}