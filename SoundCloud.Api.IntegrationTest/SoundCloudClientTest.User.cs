using System.Linq;

using NUnit.Framework;

using SoundCloud.Api.Entities;

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
        public void Test_Users_Get()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = client.Users.Get(UserId);

            Assert.That(user, Is.Not.Null);
            Assert.That(user.username, Is.EqualTo("sharpsound"));
        }

        [Test]
        public void Test_Users_GetComments()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var comments = client.Users.GetComments(user);

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public void Test_Users_GetFavorites()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var favorites = client.Users.GetFavorites(user);

            Assert.That(favorites.Any(), Is.True);
        }

        [Test]
        public void Test_Users_GetFollowers()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var followers = client.Users.GetFollowers(user);

            Assert.That(followers.Any(), Is.True);
        }

        [Test]
        public void Test_Users_GetFollowings()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var followings = client.Users.GetFollowings(user);

            Assert.That(followings.Any(), Is.True);
        }

        [Test]
        public void Test_Users_GetGroups()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var groups = client.Users.GetGroups(user).ToList();

            Assert.That(groups.Any(), Is.True);

            var sampleGroup = groups.FirstOrDefault(x => x.name == SharpSoundGroupName);

            Assert.That(sampleGroup, Is.Not.Null);
        }

        [Test]
        public void Test_Users_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var users = client.Users.Get().Take(150).ToList();

            Assert.That(users.Count, Is.EqualTo(150));
        }

        [Test]
        public void Test_Users_GetPlaylists()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var playlists = client.Users.GetPlaylists(user).ToList();

            Assert.That(playlists.Any(), Is.True);
        }

        [Test]
        public void Test_Users_GetTracks()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var tracks = client.Users.GetTracks(user);

            Assert.That(tracks.Any(), Is.True);
        }

        [Test]
        public void Test_Users_GetWebProfiles()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User();
            user.id = UserId;

            var groups = client.Users.GetWebProfiles(user).ToList();

            Assert.That(groups.Any(), Is.True);
        }
    }
}