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
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = await client.Users.GetAsync(UserId);

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo("sharpsound"));
        }

        [Test]
        public async Task Users_GetComments()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetCommentsAsync(user);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetFavorites()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetFavoritesAsync(user);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetFollowers()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetFollowersAsync(user);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetFollowings()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetFollowingsAsync(user);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var result = await client.Users.GetAllAsync();
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetPlaylists()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetPlaylistsAsync(user);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetTracks()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetTracksAsync(user);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Users_GetWebProfiles()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var user = new User { Id = UserId };

            var result = await client.Users.GetWebProfilesAsync(user);
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
