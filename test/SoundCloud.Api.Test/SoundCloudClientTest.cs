using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.Test
{
    [TestFixture]
    public class SoundCloudClientTest
    {
        private const string ClientId = "myClientId";
        private const string Token = "myTokenId";

        [Test]
        public void Test_Apps()
        {
            var client = SoundCloudClient.Create();

            var apps = client.Apps;

            Assert.That(apps, Is.Not.Null);
        }

        [Test]
        public void Test_ClientId()
        {
            var client = SoundCloudClient.Create();

            client.ClientId = ClientId;

            Assert.That(client.ClientId, Is.EqualTo(ClientId));
        }

        [Test]
        public void Test_Comments()
        {
            var client = SoundCloudClient.Create();

            var comments = client.Comments;

            Assert.That(comments, Is.Not.Null);
        }

        [Test]
        public void Test_CreateWithClientId()
        {
            var client = SoundCloudClient.CreateUnauthorized(ClientId);
            Assert.That(client.IsAuthorized, Is.False);
        }

        [Test]
        public void Test_CreateWithToken()
        {
            var client = SoundCloudClient.CreateAuthorized(Token);
            Assert.That(client.IsAuthorized, Is.True);
        }

        [Test]
        public void Test_EnsureClientId()
        {
            var client = SoundCloudClient.Create();

            Assert.Throws<SoundCloudInsufficientAccessRightsException>(() => client.Tracks.Get());
        }

        [Test]
        public void Test_EnsureToken()
        {
            var client = SoundCloudClient.Create();

            Assert.Throws<SoundCloudInsufficientAccessRightsException>(() => client.Tracks.Update(new Track()));
        }

        [Test]
        public void Test_Groups()
        {
            var client = SoundCloudClient.Create();

            var groups = client.Groups;

            Assert.That(groups, Is.Not.Null);
        }

        [Test]
        public void Test_IsAuthorized()
        {
            var client = SoundCloudClient.Create();

            client.Token = Token;

            Assert.That(client.IsAuthorized, Is.True);
        }

        [Test]
        public void Test_Me()
        {
            var client = SoundCloudClient.Create();

            var me = client.Me;

            Assert.That(me, Is.Not.Null);
        }

        [Test]
        public void Test_OAuth()
        {
            var client = SoundCloudClient.Create();

            var oAuth2 = client.OAuth2;

            Assert.That(oAuth2, Is.Not.Null);
        }

        [Test]
        public void Test_Playlist()
        {
            var client = SoundCloudClient.Create();

            var playlists = client.Playlists;

            Assert.That(playlists, Is.Not.Null);
        }

        [Test]
        public void Test_Resolve()
        {
            var client = SoundCloudClient.Create();

            var resolve = client.Resolve;

            Assert.That(resolve, Is.Not.Null);
        }

        [Test]
        public void Test_Token()
        {
            var client = SoundCloudClient.Create();

            client.Token = Token;

            Assert.That(client.Token, Is.EqualTo(Token));
        }

        [Test]
        public void Test_Track()
        {
            var client = SoundCloudClient.Create();

            var tracks = client.Tracks;

            Assert.That(tracks, Is.Not.Null);
        }

        [Test]
        public void Test_User()
        {
            var client = SoundCloudClient.Create();

            var user = client.Users;

            Assert.That(user, Is.Not.Null);
        }
    }
}