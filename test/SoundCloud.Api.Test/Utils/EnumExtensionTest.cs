using System;
using NUnit.Framework;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Utils
{
    [TestFixture]
    public class EnumExtensionTest
    {
        private enum TestEnum
        {
            [Baz(Value = "FooBar")]
            Foo,
            Bar
        }

        private class BazAttribute : Attribute
        {
            public string Value { get; set; }
        }

        [Test]
        public void Test_GetAttributeOfType_Available()
        {
            var attribute = TestEnum.Foo.GetAttributeOfType<BazAttribute>();

            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Value, Is.EqualTo("FooBar"));
        }

        [Test]
        public void Test_GetAttributeOfType_Not_Available()
        {
            var attribute = TestEnum.Bar.GetAttributeOfType<BazAttribute>();

            Assert.That(attribute, Is.Null);
        }
    }
}