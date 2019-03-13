using Newtonsoft.Json;

using NUnit.Framework;

using SoundCloud.Api.Json;

namespace SoundCloud.Api.Test.Json
{
    [TestFixture]
    public class JsonIgnoreOnSerializeAttributeTest
    {
        [Test]
        public void Test_JsonIgnoreOnSerializeAttribute()
        {
            var testObject = new TestClass();
            testObject.Property = "Property";
            testObject.IgnoredProperty = "IgnoredProperty";

            var settings = new JsonSerializerSettings {ContractResolver = new SpecialContractResolver()};

            var json = JsonConvert.SerializeObject(testObject, settings);

            Assert.That(json, Does.Not.Contain("IgnoredProperty"));
            Assert.That(json, Does.Contain("Property"));
        }

        private class TestClass
        {
            [JsonIgnoreOnSerialize]
            public string IgnoredProperty { get; set; }

            public string Property { get; set; }
        }
    }
}