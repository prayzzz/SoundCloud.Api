using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SoundCloud.Api.Entities;

namespace SoundCloud.Api.Json
{
    internal sealed class PlaylistTracksJsonConverter : JsonConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<Track>);
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        ///     The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, objectType);
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IEnumerable<Track>))
            {
                throw new ArgumentException("Value is no list of tracks", "value");
            }

            var entity = (IEnumerable<Track>) value;

            writer.WriteStartArray();
            foreach (var track in entity)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("id");
                writer.WriteValue(track.Id);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }
    }
}