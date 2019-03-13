using System.Text;

using NUnit.Framework;

using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Utils
{
    [TestFixture]
    public class StringExtensionTest
    {
        [Test]
        public void Test_AppendLineIfNotEmpty()
        {
            var builder = new StringBuilder();
            builder.AppendLine("foo");
            builder.AppendLineIfNotEmpty("bar");

            Assert.That(builder.ToString(), Is.EqualTo("foo\r\nbar\r\n"));
        }

        [Test]
        public void Test_AppendLineIfNotEmpty_Empty_String()
        {
            var builder = new StringBuilder();
            builder.AppendLine("foo");
            builder.AppendLineIfNotEmpty(string.Empty);

            Assert.That(builder.ToString(), Is.EqualTo("foo\r\n"));
        }

        [Test]
        public void Test_GetBytes()
        {
            var b = "foo".GetBytes();

            Assert.That(b, Is.EqualTo("foo".GetBytes()));
        }
    }
}