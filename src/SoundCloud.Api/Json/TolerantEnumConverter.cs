using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Json
{
    internal sealed class TolerantEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
            return type.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullable = IsNullableType(objectType);
            var enumType = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            var enumValues = Enum.GetValues(enumType).Cast<Enum>();
            var enumNames = new Dictionary<string, Enum>();
            foreach (var enumVal in enumValues)
            {
                var attr = enumVal.GetAttributeOfType<EnumMemberAttribute>();

                if (attr == null)
                {
                    enumNames.Add(enumVal.ToString(), enumVal);
                    continue;
                }

                enumNames.Add(attr.Value, enumVal);
            }

            if (reader.TokenType == JsonToken.String)
            {
                var enumText = reader.Value.ToString();

                if (!string.IsNullOrEmpty(enumText))
                {
                    Enum enumVal;
                    if (enumNames.TryGetValue(enumText, out enumVal))
                    {
                        return enumVal;
                    }
                }
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                var enumVal = Convert.ToInt32(reader.Value);
                var values = (int[]) Enum.GetValues(enumType);
                if (values.Contains(enumVal))
                {
                    return Enum.Parse(enumType, enumVal.ToString());
                }
            }

            if (!isNullable)
            {
                var names = Enum.GetNames(enumType);
                var defaultName = names.FirstOrDefault(n => string.Equals(n, "None", StringComparison.OrdinalIgnoreCase)) ?? names.First();

                return Enum.Parse(enumType, defaultName);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var val = value as Enum;

            if (val == null)
            {
                writer.WriteNull();
                return;
            }

            var enumMemberAttribute = val.GetAttributeOfType<EnumMemberAttribute>();

            if (enumMemberAttribute == null)
            {
                writer.WriteValue(val.ToString());
                return;
            }

            writer.WriteValue(enumMemberAttribute.Value);
        }

        private bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}