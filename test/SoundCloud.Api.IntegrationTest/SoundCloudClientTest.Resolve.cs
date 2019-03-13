using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;

namespace SoundCloud.Api.IntegrationTest
{
    /// <summary>
    /// This class tests the logic of the wrapper against the real SoundCloud API.
    /// Therefore a clientid and token is needed. Both values are loaded from a settings.json file.
    /// In order to run this tests, the file must be provided.
    /// All tests are marked as inconclusive, if the file is not available.
    /// </summary>
    [TestFixture]
    public partial class SoundCloudClientTest
    {
        [Test]
        public void Test_Resolve_GetUrl()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var result = client.Resolve.GetEntity("https://soundcloud.com/sharpsound-2");

            Assert.That(result.kind, Is.EqualTo(Kind.User));
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void Test_Resolve_GetUrl_Wrong_Url()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var result = client.Resolve.GetEntity("https://soundcloud.com/sharpsound-12345");

            Assert.That(result, Is.Null);
        }
    }
}