using NUnit.Framework;

namespace SoundCloud.Api.Test
{
    [TestFixture]
    public class SoundCloudClientTest
    {
        private const string ClientId = "myClientId";

        [Test]
        public void Test_Apps()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var apps = client.Apps;

            Assert.That(apps, Is.Not.Null);
        }

        [Test]
        public void Test_Comments()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var comments = client.Comments;

            Assert.That(comments, Is.Not.Null);
        }

        [Test]
        public void Test_Groups()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var groups = client.Groups;

            Assert.That(groups, Is.Not.Null);
        }

        [Test]
        public void Test_Me()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var me = client.Me;

            Assert.That(me, Is.Not.Null);
        }

        [Test]
        public void Test_OAuth()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var oAuth2 = client.OAuth2;

            Assert.That(oAuth2, Is.Not.Null);
        }

        [Test]
        public void Test_Playlist()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var playlists = client.Playlists;

            Assert.That(playlists, Is.Not.Null);
        }

        [Test]
        public void Test_Resolve()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var resolve = client.Resolve;

            Assert.That(resolve, Is.Not.Null);
        }

        [Test]
        public void Test_Track()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var tracks = client.Tracks;

            Assert.That(tracks, Is.Not.Null);
        }

        [Test]
        public void Test_User()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);

            var user = client.Users;

            Assert.That(user, Is.Not.Null);
        }
    }
}