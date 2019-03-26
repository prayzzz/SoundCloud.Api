using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class AppsTest : SoundCloudClientTest
    {
        [Test]
        public async Task Test_Apps_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var appToGet = (await client.Apps.GetAsync()).First();

            var app = await client.Apps.GetAsync(appToGet.Id);

            Assert.That(app, Is.Not.Null);
        }

        /// <summary>
        ///     Some extended testing to ensure functionality of <see cref="SoundCloudList{T}" /> against live api.
        /// </summary>
        [Test]
        public async Task Test_Apps_GetList()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var appClients = await client.Apps.GetAsync();

            var someApps = appClients.Take(100).ToList();
            Assert.That(someApps.Count, Is.EqualTo(100));

            var sameApps = appClients.Take(100).ToList();
            Assert.That(sameApps.Count, Is.EqualTo(100));
            Assert.AreSame(someApps[0], sameApps[0]);
            Assert.AreSame(someApps[99], sameApps[99]);

            var moreApps = appClients.Skip(100).Take(200).ToList();
            Assert.That(moreApps.Count, Is.EqualTo(200));
        }
    }
}