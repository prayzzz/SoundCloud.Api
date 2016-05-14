using System.Reflection;

using Newtonsoft.Json.Serialization;

namespace SoundCloud.Api.Json
{
    public class NullBoolValueProvider : IValueProvider
    {
        private readonly IValueProvider _underlyingValueProvider;

        public NullBoolValueProvider(MemberInfo memberInfo)
        {
            _underlyingValueProvider = new DynamicValueProvider(memberInfo);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? false;
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value ?? false);
        }
    }
}