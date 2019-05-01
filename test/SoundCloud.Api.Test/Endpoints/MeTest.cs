using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class MeTest
    {
        private const int TrackId = 215850263;
        private const int UserId = 164386753;

        [Test]
        public async Task DeleteWebProfile()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/web-profiles/123?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var profile = new WebProfile { Id = 123 };
            var result = await new Me(gatewayMock.Object).DeleteWebProfileAsync(profile);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }

        [Test]
        public async Task Follow()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings/164386753?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendPutRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var userToFollow = new User { Id = UserId };
            var result = await new Me(gatewayMock.Object).FollowAsync(userToFollow);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var user = new User();
            gatewayMock.Setup(x => x.SendGetRequestAsync<User>(expectedUri)).ReturnsAsync(user);

            // Act
            var result = await new Me(gatewayMock.Object).GetAsync();

            // Assert
            Assert.That(result, Is.SameAs(user));
        }

        [Test]
        public async Task GetActivities()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/activities?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var activities = new PagedResult<Activity> { Collection = new List<Activity> { new Activity(), new Activity() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Activity>>(expectedUri)).ReturnsAsync(activities);

            // Act
            var result = (await new Me(gatewayMock.Object).GetActivitiesAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(activities.Collection));
        }

        [Test]
        public async Task GetComments()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/comments?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var commentList = new PagedResult<Comment> { Collection = new List<Comment> { new Comment(), new Comment() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(commentList);

            // Act
            var result = (await new Me(gatewayMock.Object).GetCommentsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(commentList.Collection));
        }

        [Test]
        public async Task GetConnections()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/connections?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var connections = new PagedResult<Connection> { Collection = new List<Connection> { new Connection(), new Connection() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Connection>>(expectedUri)).ReturnsAsync(connections);

            // Act
            var result = (await new Me(gatewayMock.Object).GetConnectionsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(connections.Collection));
        }

        [Test]
        public async Task GetFavorites()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/favorites?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = new List<Track> { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var result = (await new Me(gatewayMock.Object).GetFavoritesAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task GetFollowers()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followers?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var followers = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(followers);

            // Act
            var result = (await new Me(gatewayMock.Object).GetFollowersAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(followers.Collection));
        }

        [Test]
        public async Task GetFollowings()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var followings = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(followings);

            // Act
            var result = (await new Me(gatewayMock.Object).GetFollowingsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(followings.Collection));
        }

        [Test]
        public async Task GetPlaylists()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/playlists?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlists = new PagedResult<Playlist> { Collection = new List<Playlist> { new Playlist(), new Playlist() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(playlists);

            // Act
            var result = (await new Me(gatewayMock.Object).GetPlaylistsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(playlists.Collection));
        }

        [Test]
        public async Task GetTracks()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/tracks?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = new List<Track> { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var result = (await new Me(gatewayMock.Object).GetTracksAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task GetWebProfiles()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/web-profiles?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var webProfiles = new PagedResult<WebProfile> { Collection = new List<WebProfile> { new WebProfile(), new WebProfile() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<WebProfile>>(expectedUri)).ReturnsAsync(webProfiles);

            // Act
            var result = (await new Me(gatewayMock.Object).GetWebProfilesAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(webProfiles.Collection));
        }

        [Test]
        public async Task Like()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/favorites/215850263?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendPutRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var trackToLike = new Track { Id = TrackId };
            var result = await new Me(gatewayMock.Object).LikeAsync(trackToLike);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }

        [Test]
        public async Task PostWebProfile()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/web-profiles?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var profile = new WebProfile { Title = "title", Url = "url" };
            var postedProfile = new WebProfile { Id = 123, Title = "title", Url = "url" };
            gatewayMock.Setup(x => x.SendPostRequestAsync<WebProfile>(expectedUri, profile)).ReturnsAsync(postedProfile);

            // Act
            var result = await new Me(gatewayMock.Object).PostWebProfileAsync(profile);

            // Assert
            Assert.That(result, Is.SameAs(postedProfile));
        }

        [Test]
        public async Task Unfollow()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings/164386753?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var userToUnfollow = new User { Id = UserId };
            var result = await new Me(gatewayMock.Object).UnfollowAsync(userToUnfollow);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }

        [Test]
        public async Task Unlike()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/favorites/215850263?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var trackToLike = new Track { Id = TrackId };
            var result = await new Me(gatewayMock.Object).UnlikeAsync(trackToLike);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }
    }
}
