using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class ResolveTest : SoundCloudClientTest
    {
        [Test]
        public async Task Resolve_GetUrl()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var result = await client.Resolve.GetEntityAsync("https://soundcloud.com/sharpsound-2");

            Assert.That(result.Kind, Is.EqualTo(Kind.User));
            Assert.That(result, Is.TypeOf<User>());
        }

        [Test]
        public void Resolve_GetUrl_Wrong_Url()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var exception =
                Assert.ThrowsAsync<SoundCloudApiException>(async () =>
                    await client.Resolve.GetEntityAsync("https://soundcloud.com/sharpsound-12345"));
            Assert.That(exception.HttpStatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}