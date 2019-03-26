using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace SoundCloud.Api.Json
{
    internal sealed class NullStringValueProvider : IValueProvider
    {
        private readonly IValueProvider _underlyingValueProvider;

        public NullStringValueProvider(MemberInfo memberInfo)
        {
            _underlyingValueProvider = new ReflectionValueProvider(memberInfo);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? string.Empty;
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value ?? string.Empty);
        }
    }
}