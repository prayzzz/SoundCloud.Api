using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using Moq;

using NUnit.Framework;

using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Test.Data;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class GroupsTest
    {
        private const string ClientId = "myClientId";
        private const int GroupId = 215200;
        private const string Token = "myTokenId";
        private const int TrackId = 215850263;

        [Test]
        public void Test_Groups_Delete()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200?oauth_token=myTokenId";

            var group = new Group();
            group.id = 215200;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.Delete(group);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Groups_DeleteContribution()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/contributions/215850263?oauth_token=myTokenId";

            var group = new Group();
            group.id = 215200;

            var track = new Track();
            track.id = TrackId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.DeleteContribution(group, track);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Groups_DeletePendingTrack()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/pending_tracks/215850263?oauth_token=myTokenId";

            var group = new Group();
            group.id = 215200;

            var track = new Track();
            track.id = TrackId;

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.DeletePendingTrack(group, track);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Groups_Get()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200?client_id=myClientId";

            var group = new Group();

            var response = new ApiResponse<Group>(HttpStatusCode.OK, "OK");
            response.Data = group;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<Group>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var result = groupsEndpoint.Get(GroupId);

            Assert.That(result, Is.EqualTo(group));
        }

        [Test]
        public void Test_Groups_GetContributions()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/contributions?limit=50&linked_partitioning=1&oauth_token=myTokenId";

            var contributions = new PagedResult<Track>();
            contributions.collection = new List<Track> {new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = contributions;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetContributions(group);

            Assert.That(result, Is.EqualTo(contributions.collection));
        }

        [Test]
        public void Test_Groups_GetContributors()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/contributors?limit=50&linked_partitioning=1&oauth_token=myTokenId";

            var contributors = new PagedResult<User>();
            contributors.collection = new List<User> {new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = contributors;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetContributors(group);

            Assert.That(result, Is.EqualTo(contributors.collection));
        }

        [Test]
        public void Test_Groups_GetList()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups?limit=50&linked_partitioning=1&client_id=myClientId";

            var groups = new PagedResult<Group>();
            groups.collection = new List<Group> {new Group()};

            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, "OK");
            response.Data = groups;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Group>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var result = groupsEndpoint.Get();

            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public void Test_Groups_GetList_Query()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups?limit=50&q=group&linked_partitioning=1&client_id=myClientId";

            var groups = new PagedResult<Group>();
            groups.collection = new List<Group> {new Group()};

            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, "OK");
            response.Data = groups;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Group>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var builder = new GroupQueryBuilder();
            builder.SearchString = "group";

            var result = groupsEndpoint.Get(builder);

            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public void Test_Groups_GetMembers()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/members?limit=50&linked_partitioning=1&client_id=myClientId";

            var members = new PagedResult<User>();
            members.collection = new List<User> {new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = members;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetMembers(group);

            Assert.That(result, Is.EqualTo(members.collection));
        }

        [Test]
        public void Test_Groups_GetModerators()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/moderators?limit=50&linked_partitioning=1&client_id=myClientId";

            var moderators = new PagedResult<User>();
            moderators.collection = new List<User> {new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = moderators;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetModerators(group);

            Assert.That(result, Is.EqualTo(moderators.collection));
        }

        [Test]
        public void Test_Groups_GetPendingTracks()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/pending_tracks?limit=50&linked_partitioning=1&oauth_token=myTokenId";

            var tracks = new PagedResult<Track>();
            tracks.collection = new List<Track> {new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = tracks;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetPendingTracks(group);

            Assert.That(result, Is.EqualTo(tracks.collection));
        }

        [Test]
        public void Test_Groups_GetTracks()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/tracks?limit=50&linked_partitioning=1&client_id=myClientId";

            var tracks = new PagedResult<Track>();
            tracks.collection = new List<Track> {new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = tracks;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetTracks(group);

            Assert.That(result, Is.EqualTo(tracks.collection));
        }

        [Test]
        public void Test_Groups_GetUsers()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/users?limit=50&linked_partitioning=1&client_id=myClientId";

            var members = new PagedResult<User>();
            members.collection = new List<User> {new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = members;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.ClientId = ClientId;

            var group = new Group();
            group.id = GroupId;

            var result = groupsEndpoint.GetUsers(group);

            Assert.That(result, Is.EqualTo(members.collection));
        }

        [Test]
        public void Test_Groups_Post()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups?oauth_token=myTokenId";

            var group = new Group();
            group.name = "name";

            var postedGroup = new Group();
            postedGroup.id = 1;
            postedGroup.name = group.name;

            var response = new ApiResponse<Group>(HttpStatusCode.OK, "OK");
            response.Data = postedGroup;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Group>(It.Is<Uri>(y => y.ToString() == expectedUri), group)).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.Post(group);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedGroup));
        }

        [Test]
        public void Test_Groups_Post_Track()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200/contributions?oauth_token=myTokenId";

            var group = new Group();
            group.id = 215200;

            var track = new Track();
            track.id = TrackId;

            var addedTrack = new Track();
            addedTrack.id = track.id;

            var response = new ApiResponse<Track>(HttpStatusCode.OK, "OK");
            response.Data = addedTrack;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Track>(It.Is<Uri>(y => y.ToString() == expectedUri), It.Is<Dictionary<string, object>>(y => y.ContainsValue(track.id))))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p["track[id]"], Is.EqualTo(track.id));
                });

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.Post(group, track);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(addedTrack));
        }

        [Test]
        public void Test_Groups_Update()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200?oauth_token=myTokenId";

            var group = new Group();
            group.id = 215200;
            group.name = "name";

            var updatedGroup = new Group();
            updatedGroup.id = group.id;
            updatedGroup.name = group.name;

            var response = new ApiResponse<Group>(HttpStatusCode.OK, "OK");
            response.Data = updatedGroup;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<Group>(It.Is<Uri>(y => y.ToString() == expectedUri), group)).Returns(response);

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.Update(group);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedGroup));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public void Test_Groups_UploadArtwork()
        {
            const string expectedUri = @"https://api.soundcloud.com/groups/215200?oauth_token=myTokenId";

            var group = new Group();
            group.id = 215200;
            group.name = "name";

            var updatedGroup = new Group();
            updatedGroup.id = group.id;
            updatedGroup.name = group.name;
            updatedGroup.artwork_url = "http://sampleurl.com";

            var response = new ApiResponse<Group>(HttpStatusCode.OK, "OK");
            response.Data = updatedGroup;

            var artwork = TestDataProvider.GetArtwork();

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<Group>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<Dictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p["group[artwork_data]"], Is.EqualTo(artwork));
                });

            var groupsEndpoint = new Groups(gatewayMock.Object);
            groupsEndpoint.Credentials.AccessToken = Token;

            var result = groupsEndpoint.UploadArtwork(group, artwork);
            artwork.Dispose();

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedGroup));
        }
    }
}