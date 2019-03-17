using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class UserTest
    {
        private const int UserId = 164386753;

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753?");

            var user = new User();
            var response = new ApiResponse<User>(HttpStatusCode.OK, user);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<User>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Users(gatewayMock.Object).GetAsync(UserId);

            // Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task GetComments()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/comments?limit=200&linked_partitioning=1");

            var commentList = new PagedResult<Comment> { collection = new List<Comment> { new Comment(), new Comment() } };
            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, commentList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetCommentsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(commentList.collection));
        }

        [Test]
        public async Task GetFavorites()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/favorites?limit=200&linked_partitioning=1");

            var trackList = new PagedResult<Track> { collection = new List<Track> { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetFavoritesAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public async Task GetFollowers()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/followers?limit=200&linked_partitioning=1");

            var followers = new PagedResult<User> { collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, followers);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetFollowersAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(followers.collection));
        }

        [Test]
        public async Task GetFollowings()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/followings?limit=200&linked_partitioning=1");

            var followings = new PagedResult<User> { collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, followings);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetFollowingsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(followings.collection));
        }

        [Test]
        public async Task GetGroups()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/groups?limit=200&linked_partitioning=1");

            var groups = new PagedResult<Group> { collection = new List<Group> { new Group(), new Group() } };
            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, groups);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Group>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetGroupsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users?limit=200&linked_partitioning=1");

            var userList = new PagedResult<User> { collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, userList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Users(gatewayMock.Object).GetAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(userList.collection));
        }

        [Test]
        public async Task GetList_With_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users?limit=100&q=SharpSound&linked_partitioning=1");

            var userList = new PagedResult<User> { collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, userList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var builder = new UserQueryBuilder { Limit = 100, SearchString = "SharpSound" };
            var result = (await new Users(gatewayMock.Object).GetAsync(builder)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(userList.collection));
        }

        [Test]
        public async Task GetPlaylists()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/playlists?limit=200&linked_partitioning=1");

            var playlists = new PagedResult<Playlist> { collection = new List<Playlist> { new Playlist(), new Playlist() } };
            var response = new ApiResponse<PagedResult<Playlist>>(HttpStatusCode.OK, playlists);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetPlaylistsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(playlists.collection));
        }

        [Test]
        public async Task GetTracks()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/tracks?limit=200&linked_partitioning=1");

            var trackList = new PagedResult<Track> { collection = new List<Track> { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetTracksAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public async Task GetWebProfiles()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/web-profiles?limit=200&linked_partitioning=1");

            var webProfiles = new PagedResult<WebProfile> { collection = new List<WebProfile> { new WebProfile(), new WebProfile() } };
            var response = new ApiResponse<PagedResult<WebProfile>>(HttpStatusCode.OK, webProfiles);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<WebProfile>>(expectedUri)).ReturnsAsync(response);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetWebProfilesAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(webProfiles.collection));
        }
    }
}