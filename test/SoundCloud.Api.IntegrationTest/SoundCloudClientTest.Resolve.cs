using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class ResolveTest : SoundCloudClientTest
    {
        [Test]
        public async Task Resolve_GetUrl()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var result = await client.Resolve.GetEntityAsync("https://soundcloud.com/sharpsound-2");

            Assert.That(result.Kind, Is.EqualTo(Kind.User));
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public async Task Resolve_GetUrl_Wrong_Url()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var result = await client.Resolve.GetEntityAsync("https://soundcloud.com/sharpsound-12345");

            Assert.That(result, Is.Null);
        }
    }
}