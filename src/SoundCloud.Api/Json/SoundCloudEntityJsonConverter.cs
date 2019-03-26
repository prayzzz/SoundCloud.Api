using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Json
{
    internal sealed class SoundCloudEntityJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Entity).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = (JObject) serializer.Deserialize(reader);

            if (jsonObject == null)
            {
                return null;
            }

            var kindToken = jsonObject.GetValue("kind");

            if (kindToken == null)
            {
                return jsonObject.ToObject(objectType);
            }

            var kind = EnumHelper.ParseTolerant<Kind>(kindToken.Value<string>());

            switch (kind)
            {
                case Kind.None:
                    return jsonObject.ToObject(objectType);
                case Kind.User:
                    return jsonObject.ToObject<User>();
                case Kind.Comment:
                    return jsonObject.ToObject<Comment>();
                case Kind.Connection:
                    return jsonObject.ToObject<Connection>();
                case Kind.Track:
                    return jsonObject.ToObject<Track>();
                case Kind.SecretToken:
                    return jsonObject.ToObject<SecretToken>();
                case Kind.Playlist:
                    return jsonObject.ToObject<Playlist>();
                case Kind.App:
                    return jsonObject.ToObject<AppClient>();
                case Kind.WebProfile:
                    return jsonObject.ToObject<WebProfile>();
                default:
                    // ReSharper disable once NotResolvedInText
                    throw new ArgumentOutOfRangeException("kind", kind, $"{kind} Not supported.");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}