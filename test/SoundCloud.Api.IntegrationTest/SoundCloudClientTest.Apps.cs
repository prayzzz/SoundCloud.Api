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
            var apps = await client.Apps.GetAllAsync();

            var result = await client.Apps.GetAsync(apps.First().Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(apps.First().Id));
        }

        [Test]
        public async Task Test_Apps_GetList()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var result = await client.Apps.GetAllAsync();
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }
    }
}
