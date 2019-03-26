using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SoundCloud.Api.Json
{
    internal sealed class StringToListJsonConverter : JsonConverter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SharpSound.SoundCloud.Json.StringToListConverter" /> class.
        ///     Default values:
        ///     Separator: ", "
        ///     EscapeChar; '"'
        /// </summary>
        public StringToListJsonConverter()
            : this(',', '"')
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:SharpSound.SoundCloud.Json.StringToListConverter" /> class.
        /// </summary>
        public StringToListJsonConverter(char separator, char escapeChar)
        {
            Separator = separator;
            EscapeChar = escapeChar;
        }

        /// <summary>
        ///     Gets or sets the escape char.
        ///     This char is places around the string if it contains the separator.
        /// </summary>
        public char EscapeChar { get; set; }

        /// <summary>
        ///     Gets or sets the string Separator.
        /// </summary>
        public char Separator { get; set; }

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
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
        /// <returns>
        ///     The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType != typeof(List<string>))
            {
                throw new JsonSerializationException("Expected type is not List<string>");
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException("Cannot convert non Strings to List<string>");
            }

            var values = reader.Value.ToString();

            var regex = new Regex(string.Format("[^{0}{1}]+|{1}([^\\{1}]*){1}", Separator, EscapeChar));
            var matches = regex.Matches(values);
            var matchList = new List<string>();
            foreach (Match match in matches)
            {
                // Add double-quoted string without the quotes
                if (match.Groups[1].Success)
                {
                    matchList.Add(match.Groups[1].Value);
                    continue;
                }

                // Add unquoted word
                matchList.Add(match.Groups[0].Value);
            }

            return matchList;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is IEnumerable<string>))
            {
                writer.WriteNull();
                return;
            }

            var list = (IEnumerable<string>) value;
            var vals = list.Select(x => x.Contains(Separator) ? EscapeChar + x + EscapeChar : x);

            writer.WriteValue(string.Join(Separator.ToString(), vals));
        }
    }
}