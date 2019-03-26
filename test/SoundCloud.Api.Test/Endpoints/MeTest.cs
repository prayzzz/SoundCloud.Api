using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var profile = new WebProfile { Id = 123 };
            var result = await new Me(gatewayMock.Object).DeleteWebProfileAsync(profile);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task DeleteWebProfile_Failed()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/web-profiles/123?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var profile = new WebProfile { Id = 123 };
            var result = await new Me(gatewayMock.Object).DeleteWebProfileAsync(profile);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));
        }

        [Test]
        public async Task Follow()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings/164386753?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var userToFollow = new User { Id = UserId };
            var result = await new Me(gatewayMock.Object).FollowAsync(userToFollow);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task Follow_Failed()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings/164386753?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Assert
            var userToFollow = new User { Id = UserId };
            var result = await new Me(gatewayMock.Object).FollowAsync(userToFollow);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me?");

            var response = new ApiResponse<User>(HttpStatusCode.OK, new User());

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<User>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Me(gatewayMock.Object).GetAsync();

            // Assert
            Assert.That(result, Is.EqualTo(response.Data));
        }

        [Test]
        public async Task GetActivities()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/activities?limit=200&linked_partitioning=1");

            var activities = new PagedResult<Activity> { Collection = new List<Activity> { new Activity(), new Activity() } };
            var response = new ApiResponse<PagedResult<Activity>>(HttpStatusCode.OK, activities);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Activity>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetActivitiesAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(activities.Collection));
        }

        [Test]
        public async Task GetComments()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/comments?limit=200&linked_partitioning=1");

            var commentList = new PagedResult<Comment> { Collection = new List<Comment> { new Comment(), new Comment() } };

            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, commentList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetCommentsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(commentList.Collection));
        }

        [Test]
        public async Task GetConnections()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/connections?limit=200&linked_partitioning=1");

            var connections = new PagedResult<Connection> { Collection = new List<Connection> { new Connection(), new Connection() } };
            var response = new ApiResponse<PagedResult<Connection>>(HttpStatusCode.OK, connections);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Connection>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetConnectionsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(connections.Collection));
        }

        [Test]
        public async Task GetFavorites()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/favorites?limit=200&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = new List<Track> { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetFavoritesAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task GetFollowers()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followers?limit=200&linked_partitioning=1");

            var followers = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, followers);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetFollowersAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(followers.Collection));
        }

        [Test]
        public async Task GetFollowings()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings?limit=200&linked_partitioning=1");

            var followings = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, followings);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetFollowingsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(followings.Collection));
        }

        [Test]
        public async Task GetPlaylists()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/playlists?limit=200&linked_partitioning=1");

            var playlists = new PagedResult<Playlist> { Collection = new List<Playlist> { new Playlist(), new Playlist() } };
            var response = new ApiResponse<PagedResult<Playlist>>(HttpStatusCode.OK, playlists);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetPlaylistsAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(playlists.Collection));
        }

        [Test]
        public async Task GetTracks()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/tracks?limit=200&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = new List<Track> { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetTracksAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task GetWebProfiles()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/web-profiles?limit=200&linked_partitioning=1");

            var webProfiles = new PagedResult<WebProfile> { Collection = new List<WebProfile> { new WebProfile(), new WebProfile() } };
            var response = new ApiResponse<PagedResult<WebProfile>>(HttpStatusCode.OK, webProfiles);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<WebProfile>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Me(gatewayMock.Object).GetWebProfilesAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(webProfiles.Collection));
        }

        [Test]
        public async Task Like()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/favorites/215850263?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var trackToLike = new Track { Id = TrackId };
            var result = await new Me(gatewayMock.Object).LikeAsync(trackToLike);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task PostWebProfile()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/web-profiles?");

            var profile = new WebProfile { Title = "title", Url = "url" };
            var postedProfile = new WebProfile { Id = 123, Title = "title", Url = "url" };
            var response = new ApiResponse<WebProfile>(HttpStatusCode.OK, postedProfile);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<WebProfile>(expectedUri, profile)).ReturnsAsync(response);

            // Act
            var result = await new Me(gatewayMock.Object).PostWebProfileAsync(profile);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedProfile));
        }

        [Test]
        public async Task Unfollow()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/followings/164386753?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var userToUnfollow = new User { Id = UserId };
            var result = await new Me(gatewayMock.Object).UnfollowAsync(userToUnfollow);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task Unlike()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/me/favorites/215850263?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);
            
            // Act
            var trackToLike = new Track { Id = TrackId };
            var result = await new Me(gatewayMock.Object).UnlikeAsync(trackToLike);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }
    }
}