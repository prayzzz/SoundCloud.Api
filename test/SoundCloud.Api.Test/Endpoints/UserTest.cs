using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        private const string ClientId = "myClientId";
        private const int UserId = 164386753;

        [Test]
        public void Test_Users_Get()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753?client_id=myClientId";

            var user = new User();

            var response = new ApiResponse<User>(HttpStatusCode.OK, "OK");
            response.Data = user;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<User>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var result = userEndpoint.Get(UserId);

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void Test_Users_GetComments()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/comments?limit=200&linked_partitioning=1&client_id=myClientId";

            var commentList = new PagedResult<Comment>();
            commentList.collection = new List<Comment> {new Comment(), new Comment()};

            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, "OK");
            response.Data = commentList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Comment>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetComments(user).ToList();

            Assert.That(result, Is.EqualTo(commentList.collection));
        }

        [Test]
        public void Test_Users_GetFavorites()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/favorites?limit=200&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetFavorites(user).ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Users_GetFollowers()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/followers?limit=200&linked_partitioning=1&client_id=myClientId";

            var followers = new PagedResult<User>();
            followers.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = followers;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetFollowers(user).ToList();

            Assert.That(result, Is.EqualTo(followers.collection));
        }

        [Test]
        public void Test_Users_GetFollowings()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/followings?limit=200&linked_partitioning=1&client_id=myClientId";

            var followings = new PagedResult<User>();
            followings.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = followings;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetFollowings(user).ToList();

            Assert.That(result, Is.EqualTo(followings.collection));
        }

        [Test]
        public void Test_Users_GetGroups()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/groups?limit=200&linked_partitioning=1&client_id=myClientId";

            var groups = new PagedResult<Group>();
            groups.collection = new List<Group> {new Group(), new Group()};

            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, "OK");
            response.Data = groups;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Group>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetGroups(user).ToList();

            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public void Test_Users_GetList()
        {
            const string expectedUri = @"https://api.soundcloud.com/users?limit=200&linked_partitioning=1&client_id=myClientId";

            var userList = new PagedResult<User>();
            userList.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = userList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var result = userEndpoint.Get().ToList();

            Assert.That(result, Is.EqualTo(userList.collection));
        }

        [Test]
        public void Test_Users_GetList_With_Query()
        {
            const string expectedUri = @"https://api.soundcloud.com/users?limit=100&q=SharpSound&linked_partitioning=1&client_id=myClientId";

            var userList = new PagedResult<User>();
            userList.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = userList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var builder = new UserQueryBuilder();
            builder.Limit = 100;
            builder.SearchString = "SharpSound";

            var result = userEndpoint.Get(builder).ToList();

            Assert.That(result, Is.EqualTo(userList.collection));
        }

        [Test]
        public void Test_Users_GetPlaylists()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/playlists?limit=200&linked_partitioning=1&client_id=myClientId";

            var playlists = new PagedResult<Playlist>();
            playlists.collection = new List<Playlist> {new Playlist(), new Playlist()};

            var response = new ApiResponse<PagedResult<Playlist>>(HttpStatusCode.OK, "OK");
            response.Data = playlists;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Playlist>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetPlaylists(user).ToList();

            Assert.That(result, Is.EqualTo(playlists.collection));
        }

        [Test]
        public void Test_Users_GetTracks()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/tracks?limit=200&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetTracks(user).ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Users_GetWebProfiles()
        {
            const string expectedUri = @"https://api.soundcloud.com/users/164386753/web-profiles?limit=200&linked_partitioning=1&client_id=myClientId";

            var webProfiles = new PagedResult<WebProfile>();
            webProfiles.collection = new List<WebProfile> {new WebProfile(), new WebProfile()};

            var response = new ApiResponse<PagedResult<WebProfile>>(HttpStatusCode.OK, "OK");
            response.Data = webProfiles;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<WebProfile>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var userEndpoint = new Users(gatewayMock.Object);
            userEndpoint.Credentials.ClientId = ClientId;

            var user = new User();
            user.id = UserId;

            var result = userEndpoint.GetWebProfiles(user).ToList();

            Assert.That(result, Is.EqualTo(webProfiles.collection));
        }
    }
}