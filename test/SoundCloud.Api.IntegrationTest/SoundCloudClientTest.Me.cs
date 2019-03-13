using System.Linq;

using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;

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
        public void Test_Me_Follow_Unfollow()
        {
            const int userId = 66852985;

            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var user = new User();
            user.id = userId;

            var result = client.Me.Follow(user);
            Assert.That(result.IsSuccess, Is.True);

            var followings = client.Me.GetFollowings();
            Assert.That(followings.Any(x => x.id == user.id), Is.True);

            result = client.Me.Unfollow(user);
            Assert.That(result.IsSuccess, Is.True);

            followings = client.Me.GetFollowings();
            Assert.That(followings.Any(x => x.id == user.id), Is.False);
        }

        [Test]
        public void Test_Me_Follow_Unknown_User()
        {
            const int userId = 999999999;

            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var user = new User();
            user.id = userId;

            var result = client.Me.Follow(user);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("404 - Not Found"));
        }

        [Test]
        public void Test_Me_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var user = client.Me.Get();

            Assert.That(user, Is.Not.Null);
            Assert.That(user.username, Is.EqualTo("sharpsound"));

            Assert.That(user.uri.Query, Does.Contain(_settings.Token));
        }

        [Test]
        public void Test_Me_GetActivity()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var activities = client.Me.GetActivities().Take(100).ToList();

            Assert.That(activities.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetComments()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var comments = client.Me.GetComments();

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetConnections()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var connections = client.Me.GetConnections().ToList();

            Assert.That(connections.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetFavorites()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var favorites = client.Me.GetFavorites();

            Assert.That(favorites.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetFollowers()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var followers = client.Me.GetFollowers();

            Assert.That(followers.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetFollowings()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var followings = client.Me.GetFollowings();

            Assert.That(followings.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetGroups()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var groups = client.Me.GetGroups().ToList();

            Assert.That(groups.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetPlaylists()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlists = client.Me.GetPlaylists().ToList();

            Assert.That(playlists.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetTracks()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var tracks = client.Me.GetTracks();

            Assert.That(tracks.Any(), Is.True);
        }

        [Test]
        public void Test_Me_GetWebProfiles()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var groups = client.Me.GetWebProfiles().ToList();

            Assert.That(groups.Any(), Is.True);
        }

        [Test]
        public void Test_Me_Like_Unlike()
        {
            const int trackId = 211433527;

            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var track = new Track();
            track.id = trackId;

            var result = client.Me.Like(track);
            Assert.That(result.IsSuccess, Is.True);

            var favorites = client.Me.GetFavorites();
            Assert.That(favorites.Any(x => x.id == track.id), Is.True);

            result = client.Me.Unlike(track);
            Assert.That(result.IsSuccess, Is.True);

            favorites = client.Me.GetFavorites();
            Assert.That(favorites.Any(x => x.id == track.id), Is.False);
        }

        [Test]
        [Ignore("There's some huge delay in posting web profile")]
        public void Test_Me_WebProfile_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var profile = new WebProfile();
            profile.url = "http://facebook.com";
            profile.title = "Facebook";
            profile.service = WebService.Facebook;

            var postResult = client.Me.PostWebProfile(profile);

            Assert.That(postResult.IsSuccess, Is.True);
            Assert.That(postResult.Data.url, Is.EqualTo(profile.url));
            Assert.That(postResult.Data.title, Is.EqualTo(profile.title));

            var profiles = client.Me.GetWebProfiles();
            Assert.That(profiles.Any(x => x.id == postResult.Data.id), Is.True);

            var deleteResult = client.Me.DeleteWebProfile(postResult.Data);
            Assert.That(deleteResult.IsSuccess, Is.True);

            profiles = client.Me.GetWebProfiles();
            Assert.That(profiles.All(x => x.id != postResult.Data.id), Is.True);
        }
    }
}