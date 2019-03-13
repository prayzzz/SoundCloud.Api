using System;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace SoundCloud.Api.Json
{
    internal sealed class NullableValueProvider : IValueProvider
    {
        private readonly object _defaultValue;
        private readonly IValueProvider _underlyingValueProvider;

        public NullableValueProvider(MemberInfo memberInfo, Type underlyingType)
        {
            _underlyingValueProvider = new ReflectionValueProvider(memberInfo);
            _defaultValue = Activator.CreateInstance(underlyingType);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? _defaultValue;
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value ?? _defaultValue);
        }
    }
}