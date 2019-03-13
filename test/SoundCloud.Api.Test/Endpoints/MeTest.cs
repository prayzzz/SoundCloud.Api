using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        private const string Token = "myTokenId";
        private const int TrackId = 215850263;
        private const int UserId = 164386753;

        [Test]
        public void Test_Me_DeleteWebProfile()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/web-profiles/123?oauth_token=myTokenId";

            var profile = new WebProfile();
            profile.id = 123;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.DeleteWebProfile(profile);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Me_DeleteWebProfile_Failed()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/web-profiles/123?oauth_token=myTokenId";

            var profile = new WebProfile();
            profile.id = 123;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, "Not Found");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.DeleteWebProfile(profile);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Me_Follow()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/followings/164386753?oauth_token=myTokenId";

            var userToFollow = new User();
            userToFollow.id = UserId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.Follow(userToFollow);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Me_Follow_Failed()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/followings/164386753?oauth_token=myTokenId";

            var userToFollow = new User();
            userToFollow.id = UserId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, "Not Found");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.Follow(userToFollow);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Me_Get()
        {
            const string expectedUri = @"https://api.soundcloud.com/me?oauth_token=myTokenId";

            var response = new ApiResponse<User>(HttpStatusCode.OK, "OK");
            response.Data = new User();

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<User>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.Get();

            Assert.That(result, Is.EqualTo(response.Data));
        }

        [Test]
        public void Test_Me_GetActivities()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/activities?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var activites = new PagedResult<Activity>();
            activites.collection = new List<Activity> {new Activity(), new Activity()};

            var response = new ApiResponse<PagedResult<Activity>>(HttpStatusCode.OK, "OK");
            response.Data = activites;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Activity>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetActivities().ToList();

            Assert.That(result, Is.EqualTo(activites.collection));
        }

        [Test]
        public void Test_Me_GetComments()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/comments?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var commentList = new PagedResult<Comment>();
            commentList.collection = new List<Comment> {new Comment(), new Comment()};

            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, "OK");
            response.Data = commentList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Comment>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetComments().ToList();

            Assert.That(result, Is.EqualTo(commentList.collection));
        }

        [Test]
        public void Test_Me_GetConnections()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/connections?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var connections = new PagedResult<Connection>();
            connections.collection = new List<Connection> {new Connection(), new Connection()};

            var response = new ApiResponse<PagedResult<Connection>>(HttpStatusCode.OK, "OK");
            response.Data = connections;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Connection>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetConnections().ToList();

            Assert.That(result, Is.EqualTo(connections.collection));
        }

        [Test]
        public void Test_Me_GetFavorites()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/favorites?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetFavorites().ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Me_GetFollowers()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/followers?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var followers = new PagedResult<User>();
            followers.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = followers;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetFollowers().ToList();

            Assert.That(result, Is.EqualTo(followers.collection));
        }

        [Test]
        public void Test_Me_GetFollowings()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/followings?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var followings = new PagedResult<User>();
            followings.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = followings;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetFollowings().ToList();

            Assert.That(result, Is.EqualTo(followings.collection));
        }

        [Test]
        public void Test_Me_GetGroups()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/groups?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var groups = new PagedResult<Group>();
            groups.collection = new List<Group> {new Group(), new Group()};

            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, "OK");
            response.Data = groups;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Group>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetGroups().ToList();

            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public void Test_Me_GetPlaylists()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/playlists?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var playlists = new PagedResult<Playlist>();
            playlists.collection = new List<Playlist> {new Playlist(), new Playlist()};

            var response = new ApiResponse<PagedResult<Playlist>>(HttpStatusCode.OK, "OK");
            response.Data = playlists;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Playlist>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetPlaylists().ToList();

            Assert.That(result, Is.EqualTo(playlists.collection));
        }

        [Test]
        public void Test_Me_GetTracks()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/tracks?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetTracks().ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Me_GetWebProfiles()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/web-profiles?limit=200&linked_partitioning=1&oauth_token=myTokenId";

            var webProfiles = new PagedResult<WebProfile>();
            webProfiles.collection = new List<WebProfile> {new WebProfile(), new WebProfile()};

            var response = new ApiResponse<PagedResult<WebProfile>>(HttpStatusCode.OK, "OK");
            response.Data = webProfiles;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<WebProfile>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var user = new User();
            user.id = UserId;

            var result = meEndpoint.GetWebProfiles().ToList();

            Assert.That(result, Is.EqualTo(webProfiles.collection));
        }

        [Test]
        public void Test_Me_Like()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/favorites/215850263?oauth_token=myTokenId";

            var trackToLike = new Track();
            trackToLike.id = TrackId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.Like(trackToLike);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Me_PostWebProfile()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/web-profiles?oauth_token=myTokenId";

            var profile = new WebProfile();
            profile.title = "title";
            profile.url = "url";

            var postedProfile = new WebProfile();
            postedProfile.id = 123;
            postedProfile.title = "title";
            postedProfile.url = "url";

            var response = new ApiResponse<WebProfile>(HttpStatusCode.OK, "OK");
            response.Data = postedProfile;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<WebProfile>(It.Is<Uri>(y => y.ToString() == expectedUri), profile)).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.PostWebProfile(profile);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedProfile));
        }

        [Test]
        public void Test_Me_Unfollow()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/followings/164386753?oauth_token=myTokenId";

            var userToUnfollow = new User();
            userToUnfollow.id = UserId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.Unfollow(userToUnfollow);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Me_Unlike()
        {
            const string expectedUri = @"https://api.soundcloud.com/me/favorites/215850263?oauth_token=myTokenId";

            var trackToLike = new Track();
            trackToLike.id = TrackId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var meEndpoint = new Me(gatewayMock.Object);
            meEndpoint.Credentials.AccessToken = Token;

            var result = meEndpoint.Unlike(trackToLike);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }
    }
}