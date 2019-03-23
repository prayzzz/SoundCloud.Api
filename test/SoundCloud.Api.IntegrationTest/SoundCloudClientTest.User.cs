using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class UserTest : SoundCloudClientTest
    {
        [Test]
        public async Task Users_Get()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = await client.Users.GetAsync(UserId);

            Assert.That(user, Is.Not.Null);
            Assert.That(user.username, Is.EqualTo("sharpsound"));
        }

        [Test]
        public async Task Users_GetComments()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var comments = await client.Users.GetCommentsAsync(user);

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public async Task Users_GetFavorites()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var favorites = await client.Users.GetFavoritesAsync(user);

            Assert.That(favorites.Any(), Is.True);
        }

        [Test]
        public async Task Users_GetFollowers()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var followers = await client.Users.GetFollowersAsync(user);

            Assert.That(followers.Any(), Is.True);
        }

        [Test]
        public async Task Users_GetFollowings()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var followings = await client.Users.GetFollowingsAsync(user);

            Assert.That(followings.Any(), Is.True);
        }

        [Test]
        public async Task Users_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var users = (await client.Users.GetAsync()).Take(150).ToList();

            Assert.That(users.Count, Is.EqualTo(150));
        }

        [Test]
        public async Task Users_GetPlaylists()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var playlists = (await client.Users.GetPlaylistsAsync(user)).ToList();

            Assert.That(playlists.Any(), Is.True);
        }

        [Test]
        public async Task Users_GetTracks()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var tracks = await client.Users.GetTracksAsync(user);

            Assert.That(tracks.Any(), Is.True);
        }

        [Test]
        public async Task Users_GetWebProfiles()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var user = new User { Id = UserId };

            var groups = (await client.Users.GetWebProfilesAsync(user)).ToList();

            Assert.That(groups.Any(), Is.True);
        }
    }
}