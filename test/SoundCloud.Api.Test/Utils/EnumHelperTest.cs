using System.Runtime.Serialization;
using NUnit.Framework;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Utils
{
    [TestFixture]
    public class EnumHelperTest
    {
        private enum TestEnum
        {
            [EnumMember(Value = "None")]
            None = 0,

            [EnumMember(Value = "barValue")]
            Bar,
            Baz
        }

        [Test]
        public void Test_ParseTolerant_Int()
        {
            var enumMember = EnumHelper.ParseTolerant<TestEnum>(2);

            Assert.That(enumMember, Is.EqualTo(TestEnum.Baz));
        }

        [Test]
        public void Test_ParseTolerant_String_No_EnumMemberValue()
        {
            var enumMember = EnumHelper.ParseTolerant<TestEnum>("Baz");

            Assert.That(enumMember, Is.EqualTo(TestEnum.Baz));
        }

        [Test]
        public void Test_ParseTolerant_String_Not_Nullable([Values("bar", "Bar", "barvalue", "barValue", "BaRvAlUe")]
                                                           string value)
        {
            var enumMember = EnumHelper.ParseTolerant<TestEnum>(value);

            Assert.That(enumMember, Is.EqualTo(TestEnum.Bar));
        }

        [Test]
        public void Test_ParseTolerant_Wrong_Int()
        {
            var enumMember = EnumHelper.ParseTolerant<TestEnum>(90);

            Assert.That(enumMember, Is.EqualTo(TestEnum.None));
        }

        [Test]
        public void Test_ParseTolerant_Wrong_String()
        {
            var enumMember = EnumHelper.ParseTolerant<TestEnum>("foobar");

            Assert.That(enumMember, Is.EqualTo(TestEnum.None));
        }
    }
}