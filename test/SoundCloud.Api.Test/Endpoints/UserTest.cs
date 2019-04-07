using System;
using System.Collections.Generic;
using System.Linq;
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

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var user = new User();
            gatewayMock.Setup(x => x.SendGetRequestAsync<User>(expectedUri)).ReturnsAsync(user);

            // Act
            var result = await new Users(gatewayMock.Object).GetAsync(UserId);

            // Assert
            Assert.That(result, Is.SameAs(user));
        }

        [Test]
        public async Task GetComments()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/comments?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var commentList = new PagedResult<Comment> { Collection = new List<Comment> { new Comment(), new Comment() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(commentList);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetCommentsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(commentList.Collection));
        }

        [Test]
        public async Task GetFavorites()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/favorites?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = new List<Track> { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetFavoritesAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task GetFollowers()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/followers?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var followers = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(followers);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetFollowersAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(followers.Collection));
        }

        [Test]
        public async Task GetFollowings()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/followings?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var followings = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(followings);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetFollowingsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(followings.Collection));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var userList = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(userList);

            // Act
            var result = (await new Users(gatewayMock.Object).GetAllAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(userList.Collection));
        }

        [Test]
        public async Task GetList_With_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users?limit=100&q=SharpSound&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var userList = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(userList);

            // Act
            var builder = new UserQueryBuilder { Limit = 100, SearchString = "SharpSound" };
            var result = (await new Users(gatewayMock.Object).GetAsync(builder)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(userList.Collection));
        }

        [Test]
        public async Task GetPlaylists()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/playlists?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlists = new PagedResult<Playlist> { Collection = new List<Playlist> { new Playlist(), new Playlist() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(playlists);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetPlaylistsAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(playlists.Collection));
        }

        [Test]
        public async Task GetTracks()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/tracks?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = new List<Track> { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetTracksAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task GetWebProfiles()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/users/164386753/web-profiles?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var webProfiles = new PagedResult<WebProfile> { Collection = new List<WebProfile> { new WebProfile(), new WebProfile() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<WebProfile>>(expectedUri)).ReturnsAsync(webProfiles);

            // Act
            var user = new User { Id = UserId };
            var result = (await new Users(gatewayMock.Object).GetWebProfilesAsync(user)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(webProfiles.Collection));
        }
    }
}