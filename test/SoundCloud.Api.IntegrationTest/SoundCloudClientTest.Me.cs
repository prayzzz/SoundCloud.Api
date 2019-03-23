using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class MeTest : SoundCloudClientTest
    {
        [Test]
        public async Task Me_Follow_Unfollow()
        {
            const int userId = 66852985;

            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var user = new User { Id = userId };

            var result = await client.Me.FollowAsync(user);
            Assert.That(result.IsSuccess, Is.True);

            var followings = await client.Me.GetFollowingsAsync();
            Assert.That(followings.Any(x => x.Id == user.Id), Is.True);

            result = await client.Me.UnfollowAsync(user);
            Assert.That(result.IsSuccess, Is.True);

            followings = await client.Me.GetFollowingsAsync();
            Assert.That(followings.Any(x => x.Id == user.Id), Is.False);
        }

        [Test]
        public async Task Me_Follow_Unknown_User()
        {
            const int userId = 999999999;

            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var user = new User { Id = userId };

            var result = await client.Me.FollowAsync(user);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("NotFound"));
        }

        [Test]
        public async Task Me_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var user = await client.Me.GetAsync();

            Assert.That(user, Is.Not.Null);
            Assert.That(user.username, Is.EqualTo("sharpsound"));
        }

        [Test]
        public async Task Me_GetActivity()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var activities = (await client.Me.GetActivitiesAsync()).Take(100).ToList();

            Assert.That(activities.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetComments()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var comments = await client.Me.GetCommentsAsync();

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        [Ignore("Is this API broken? Always returns empty list.")]
        public async Task Me_GetConnections()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var connections = (await client.Me.GetConnectionsAsync()).ToList();

            Assert.That(connections.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetFavorites()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var favorites = await client.Me.GetFavoritesAsync();

            Assert.That(favorites.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetFollowers()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var followers = await client.Me.GetFollowersAsync();

            Assert.That(followers.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetFollowings()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var followings = await client.Me.GetFollowingsAsync();

            Assert.That(followings.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetPlaylists()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlists = (await client.Me.GetPlaylistsAsync()).ToList();

            Assert.That(playlists.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetTracks()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var tracks = await client.Me.GetTracksAsync();

            Assert.That(tracks.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetWebProfiles()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var groups = (await client.Me.GetWebProfilesAsync()).ToList();

            Assert.That(groups.Any(), Is.True);
        }

        [Test]
        public async Task Me_Like_Unlike()
        {
            const int trackId = 211433527;

            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var track = new Track { Id = trackId };

            var result = await client.Me.LikeAsync(track);
            Assert.That(result.IsSuccess, Is.True);

            var favorites = await client.Me.GetFavoritesAsync();
            Assert.That(favorites.Any(x => x.Id == track.Id), Is.True);

            result = await client.Me.UnlikeAsync(track);
            Assert.That(result.IsSuccess, Is.True);

            favorites = await client.Me.GetFavoritesAsync();
            Assert.That(favorites.Any(x => x.Id == track.Id), Is.False);
        }

        [Test]
        [Ignore("There's some huge delay in posting web profile")]
        public async Task Me_WebProfile_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var profile = new WebProfile { url = "http://facebook.com", title = "Facebook", service = WebService.Facebook };

            var postResult = await client.Me.PostWebProfileAsync(profile);

            Assert.That(postResult.IsSuccess, Is.True);
            Assert.That(postResult.Data.url, Is.EqualTo(profile.url));
            Assert.That(postResult.Data.title, Is.EqualTo(profile.title));

            var profiles = await client.Me.GetWebProfilesAsync();
            Assert.That(profiles.Any(x => x.Id == postResult.Data.Id), Is.True);

            var deleteResult = await client.Me.DeleteWebProfileAsync(postResult.Data);
            Assert.That(deleteResult.IsSuccess, Is.True);

            profiles = await client.Me.GetWebProfilesAsync();
            Assert.That(profiles.All(x => x.Id != postResult.Data.Id), Is.True);
        }
    }
}