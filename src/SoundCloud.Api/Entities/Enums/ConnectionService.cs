using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ConnectionService
    {
        [EnumMember(Value = "other")]
        Other,

        [EnumMember(Value = "facebook_profile")]
        FacebookProfile,

        [EnumMember(Value = "facebook_page")]
        FacebookPage,

        [EnumMember(Value = "twitter")]
        Twitter,

        [EnumMember(Value = "tumblr")]
        Tumblr,

        [EnumMember(Value = "google_plus")]
        GoogePlus
    }
}