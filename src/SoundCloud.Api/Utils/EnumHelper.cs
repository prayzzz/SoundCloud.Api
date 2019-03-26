using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SoundCloud.Api.Utils
{
    internal class EnumHelper
    {
        /// <summary>
        ///     Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        internal static T ParseTolerant<T>(string value)
        {
            var type = typeof(T);

            var enumEntries = Enum.GetValues(type).Cast<Enum>();
            var attributeValues = new Dictionary<string, Enum>();
            foreach (var entry in enumEntries)
            {
                var attr = entry.GetAttributeOfType<EnumMemberAttribute>();

                attributeValues.Add(entry.ToString(), entry);

                if (attr == null)
                {
                    continue;
                }

                if (!attributeValues.ContainsKey(attr.Value))
                {
                    attributeValues.Add(attr.Value, entry);
                }
            }

            var enumText = value;

            if (!string.IsNullOrEmpty(enumText))
            {
                var key = attributeValues.Keys.FirstOrDefault(x => x.Equals(enumText, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrEmpty(key))
                {
                    return (T) (object) attributeValues[key];
                }
            }

            var names = Enum.GetNames(type);
            var defaultName = names.FirstOrDefault(n => string.Equals(n, "None", StringComparison.OrdinalIgnoreCase)) ?? names.First();

            return (T) Enum.Parse(type, defaultName);
        }

        /// <summary>
        ///     Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="value">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        internal static T ParseTolerant<T>(int value)
        {
            var type = typeof(T);

            var values = (int[]) Enum.GetValues(type);
            if (values.Contains(value))
            {
                return (T) Enum.Parse(type, value.ToString());
            }

            var names = Enum.GetNames(type);
            var defaultName = names.FirstOrDefault(n => string.Equals(n, "None", StringComparison.OrdinalIgnoreCase)) ?? names.First();

            return (T) Enum.Parse(type, defaultName);
        }
    }
}