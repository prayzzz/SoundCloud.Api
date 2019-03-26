using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Json;

namespace SoundCloud.Api.Entities.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum WebService
    {
        [EnumMember(Value = "other")]
        Other = 0,

        [EnumMember(Value = "personal")]
        Personal,

        [EnumMember(Value = "bandpage")]
        BandPage,

        [EnumMember(Value = "bandsintown")]
        BandsInTown,

        [EnumMember(Value = "email")]
        Email,

        [EnumMember(Value = "facebook")]
        Facebook,

        [EnumMember(Value = "flickr")]
        Flickr,

        [EnumMember(Value = "foursquare")]
        FourSquare,

        [EnumMember(Value = "googleplus")]
        GooglePlus,

        [EnumMember(Value = "lastfm")]
        LastFm,

        [EnumMember(Value = "mixi")]
        Mixi,

        [EnumMember(Value = "myspace")]
        MySpace,

        [EnumMember(Value = "orkut")]
        Orkut,

        [EnumMember(Value = "pinterest")]
        Pinterest,

        [EnumMember(Value = "posterous")]
        Posterous,

        [EnumMember(Value = "rdio")]
        Rdio,

        [EnumMember(Value = "reddit")]
        Reddit,

        [EnumMember(Value = "songkick")]
        Songkick,

        [EnumMember(Value = "soundcloud")]
        SoundCloud,

        [EnumMember(Value = "spotify")]
        Spotify,

        [EnumMember(Value = "stumbleupon")]
        Stumpleupon,

        [EnumMember(Value = "thisismyjam")]
        ThisIsMyJam,

        [EnumMember(Value = "tuenti")]
        Tuenti,

        [EnumMember(Value = "tumblr")]
        Tumblr,

        [EnumMember(Value = "twitter")]
        Twitter,

        [EnumMember(Value = "vimeo")]
        Vimeo,

        [EnumMember(Value = "vkontakte")]
        Vkontakte,

        [EnumMember(Value = "weibo")]
        Weibo,

        [EnumMember(Value = "youtube")]
        Youtube,

        [EnumMember(Value = "instagram")]
        Instragram
    }
}