using System.Linq;

using NUnit.Framework;

using SoundCloud.Api.Utils;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public partial class SoundCloudClientTest
    {
        [Test]
        public void Test_Apps_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var appToGet = client.Apps.Get().First();

            var app = client.Apps.Get(appToGet.id);

            Assert.That(app, Is.Not.Null);
            Assert.That(app.uri.Query, Does.Contain(_settings.Token));
        }

        /// <summary>
        /// Some extended testing to ensure functionality of <see cref="SoundCloudList{T}"/> against live api.
        /// </summary>
        [Test]
        public void Test_Apps_GetList()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var appClients = client.Apps.Get();

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