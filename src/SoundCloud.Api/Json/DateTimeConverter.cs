using System;
using Newtonsoft.Json;

namespace SoundCloud.Api.Json
{
    internal sealed class DateTimeConverter : JsonConverter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SoundCloud.Api.Json.DateTimeConverter" /> class.
        /// </summary>
        public DateTimeConverter(string pattern)
        {
            Pattern = pattern;
        }

        /// <summary>
        ///     Gets or sets the DateTime pattern.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType != typeof(DateTime))
            {
                throw new JsonSerializationException("Expected type is not DateTime");
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException("Cannot convert non Strings to DateTime");
            }

            DateTime parsedDateTime;
            if (DateTime.TryParse(reader.Value.ToString(), out parsedDateTime))
            {
                return parsedDateTime;
            }

            throw new JsonSerializationException(string.Format("Error converting value {0} to type '{1}'", reader.Value, objectType));
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateTime))
            {
                writer.WriteNull();
            }

            var dateTime = (DateTime) value;
            writer.WriteValue(dateTime.ToString(Pattern));
        }
    }
}