using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SoundCloud.Api.Json
{
    internal sealed class SpecialContractResolver : DefaultContractResolver
    {
        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var pi = (PropertyInfo) member;

                if (pi.PropertyType == typeof(string))
                {
                    return new NullStringValueProvider(member);
                }

                if (pi.PropertyType == typeof(int))
                {
                    return new NullIntValueProvider(member);
                }

                if (pi.PropertyType == typeof(bool))
                {
                    return new NullBoolValueProvider(member);
                }

                if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return new NullableValueProvider(member, pi.PropertyType.GetGenericArguments().First());
                }
            }

            return base.CreateMemberValueProvider(member);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            var attributes = property.DeclaringType.GetProperty(property.UnderlyingName).GetCustomAttributes().ToList();

            if (!attributes.Any())
            {
                return property;
            }

            var ignore = attributes.Any(x => x.GetType() == typeof(JsonIgnoreOnSerializeAttribute));
            property.ShouldSerialize = x => !ignore;

            return property;
        }
    }
}