using System.Text;

using NUnit.Framework;

using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Test.Exceptions
{
    [TestFixture]
    public class SoundCloudInsufficientAccessRightsExceptionTest
    {
        [Test]
        public void Test_With_ErrorMessages()
        {
            var messages = new ValidationMessages();
            messages.Add("MyMessage");

            var ex = new SoundCloudValidationException(messages);

            Assert.That(ex.Message, Is.EqualTo("MyMessage"));
        }

        [Test]
        public void Test_With_Message()
        {
            var ex = new SoundCloudValidationException("MyMessage");

            Assert.That(ex.Message, Is.EqualTo("MyMessage"));
        }

        [Test]
        public void Test_With_StringBuilder()
        {
            var builder = new StringBuilder();
            builder.AppendLine("MyMessage");
            builder.AppendLine();

            var ex = new SoundCloudValidationException(builder);

            Assert.That(ex.Message, Is.EqualTo("MyMessage"));
        }

        [Test]
        public void Test_Without_Message()
        {
            var ex = new SoundCloudValidationException();

            Assert.That(ex.Message, Does.Contain("SoundCloud.Api.Exceptions.SoundCloudValidationException"));
        }
    }
}