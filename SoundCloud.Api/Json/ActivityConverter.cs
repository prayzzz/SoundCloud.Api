using System;
using System.ComponentModel;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Json
{
    public class ActivityConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(Entity).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = (JObject)serializer.Deserialize(reader);
            var activityType = EnumHelper.ParseTolerant<ActivityType>(jsonObject.GetValue("kind").Value<string>());

            switch (activityType)
            {
                case ActivityType.Comment:
                    return jsonObject.ToObject<Comment>();
                case ActivityType.Favoriting:
                case ActivityType.Track:
                case ActivityType.TrackRepost:
                    return jsonObject.ToObject<Track>();
                case ActivityType.Playlist:
                case ActivityType.PlaylistRepost:
                    return jsonObject.ToObject<Playlist>();
                default:
                    throw new InvalidEnumArgumentException("activityType", (int)activityType, typeof(ActivityType));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}