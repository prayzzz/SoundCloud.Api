using NUnit.Framework;

using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.Test.Exceptions
{
    public class SoundCloudApiException
    {
        [Test]
        public void Test_With_Message()
        {
            var ex = new SoundCloudInsufficientAccessRightsException("MyMessage");

            Assert.That(ex.Message, Is.EqualTo("MyMessage"));
        }

        [Test]
        public void Test_Without_Message()
        {
            var ex = new SoundCloudInsufficientAccessRightsException();

            Assert.That(ex.Message, Does.Contain("SoundCloud.Api.Exceptions.SoundCloudInsufficientAccessRightsException"));
        }
    }
}