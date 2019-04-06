using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class MeTest : SoundCloudClientTest
    {
        [Test]
        public async Task Me_Follow_Unfollow()
        {
            const int userId = 66852985;

            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var user = new User { Id = userId };

            var followResult = await client.Me.FollowAsync(user);
            Assert.That(followResult.Errors, Is.Empty);

            var followingsResult1 = await client.Me.GetFollowingsAsync();
            Assert.That(followingsResult1, Has.One.Matches<User>(x => x.Id == user.Id));

            var unfollowResult = await client.Me.UnfollowAsync(user);
            Assert.That(unfollowResult.Errors, Is.Empty);

            var followingsResult2 = await client.Me.GetFollowingsAsync();
            Assert.That(followingsResult2, Has.None.Matches<User>(x => x.Id == user.Id));
        }

        [Test]
        public void Me_Follow_Unknown_User()
        {
            const int userId = 999999999;

            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var user = new User { Id = userId };

            var exception = Assert.ThrowsAsync<SoundCloudApiException>(async () => await client.Me.FollowAsync(user));
            Assert.That(exception.HttpStatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Me_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var user = await client.Me.GetAsync();

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo("sharpsound"));
        }

        [Test]
        public async Task Me_GetActivity()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var activities = (await client.Me.GetActivitiesAsync()).Take(100).ToList();

            Assert.That(activities.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetComments()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var comments = await client.Me.GetCommentsAsync();

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        [Ignore("Is this API broken? Always returns empty list.")]
        public async Task Me_GetConnections()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var connections = (await client.Me.GetConnectionsAsync()).ToList();

            Assert.That(connections.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetFavorites()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var favorites = await client.Me.GetFavoritesAsync();

            Assert.That(favorites.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetFollowers()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var followers = await client.Me.GetFollowersAsync();

            Assert.That(followers.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetFollowings()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var followings = await client.Me.GetFollowingsAsync();

            Assert.That(followings.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetPlaylists()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlists = (await client.Me.GetPlaylistsAsync()).ToList();

            Assert.That(playlists.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetTracks()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var tracks = await client.Me.GetTracksAsync();

            Assert.That(tracks.Any(), Is.True);
        }

        [Test]
        public async Task Me_GetWebProfiles()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var groups = (await client.Me.GetWebProfilesAsync()).ToList();

            Assert.That(groups.Any(), Is.True);
        }

        [Test]
        public async Task Me_Like_Unlike()
        {
            const int trackId = 211433527;

            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var track = new Track { Id = trackId };

            var likeResult = await client.Me.LikeAsync(track);
            Assert.That(likeResult.Errors, Is.Empty);

            var favoritesResult1 = await client.Me.GetFavoritesAsync();
            Assert.That(favoritesResult1, Has.One.Matches<Track>(x => x.Id == track.Id));

            var unlikeResult = await client.Me.UnlikeAsync(track);
            Assert.That(unlikeResult.Errors, Is.Empty);

            var favoritesResult2 = await client.Me.GetFavoritesAsync();
            Assert.That(favoritesResult2, Has.None.Matches<Track>(x => x.Id == track.Id));
        }

        [Test]
        [Ignore("There's some huge delay in posting web profile")]
        public async Task Me_WebProfile_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var profile = new WebProfile { Url = "http://facebook.com", Title = "Facebook", Service = WebService.Facebook };

            var postResult = await client.Me.PostWebProfileAsync(profile);
            Assert.That(postResult.Url, Is.EqualTo(profile.Url));
            Assert.That(postResult.Title, Is.EqualTo(profile.Title));

            var profilesResult1 = await client.Me.GetWebProfilesAsync();
            Assert.That(profilesResult1, Has.One.Matches<Track>(x => x.Id == postResult.Id));

            var deleteResult = await client.Me.DeleteWebProfileAsync(postResult);
            Assert.That(deleteResult.Error, Is.Null.Or.Empty);
            Assert.That(deleteResult.Errors, Is.Empty);

            var profilesResult2 = await client.Me.GetWebProfilesAsync();
            Assert.That(profilesResult2, Has.None.Matches<Track>(x => x.Id == postResult.Id));
        }
    }
}