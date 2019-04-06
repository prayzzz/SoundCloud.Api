using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class AppsTest : SoundCloudClientTest
    {
        [Test]
        public async Task Test_Apps_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var appId = (await client.Apps.GetAllAsync()).First().Id;
            var app = await client.Apps.GetAsync(appId);

            Assert.That(app, Is.Not.Null);
        }

        [Test]
        public async Task Test_Apps_GetList()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var appClients = await client.Apps.GetAllAsync();

            Assert.That(appClients.Count, Is.EqualTo(200));
        }
    }
}