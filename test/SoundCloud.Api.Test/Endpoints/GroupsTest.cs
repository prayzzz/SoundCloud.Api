using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
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
        private const int GroupId = 215200;
        private const int TrackId = 215850263;

        [Test]
        public async Task Delete()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).DeleteAsync(group);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task DeleteContribution()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/contributions/215850263?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var track = new Track { Id = TrackId };
            var result = await new Groups(gatewayMock.Object).DeleteContributionAsync(group, track);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task DeletePendingTrack()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/pending_tracks/215850263?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var track = new Track { Id = TrackId };
            var result = await new Groups(gatewayMock.Object).DeletePendingTrackAsync(group, track);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200?");

            var group = new Group();
            var response = new ApiResponse<Group>(HttpStatusCode.OK, group);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<Group>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Groups(gatewayMock.Object).GetAsync(GroupId);

            // Assert
            Assert.That(result, Is.EqualTo(group));
        }

        [Test]
        public async Task GetContributions()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/contributions?limit=50&linked_partitioning=1");

            var contributions = new PagedResult<Track> { collection = new List<Track> { new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, contributions);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetContributionsAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(contributions.collection));
        }

        [Test]
        public async Task GetContributors()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/contributors?limit=50&linked_partitioning=1");

            var contributors = new PagedResult<User> { collection = new List<User> { new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, contributors);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetContributorsAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(contributors.collection));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups?limit=50&linked_partitioning=1");

            var groups = new PagedResult<Group> { collection = new List<Group> { new Group() } };
            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, groups);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Group>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Groups(gatewayMock.Object).GetAsync();

            // Assert
            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public async Task GetList_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups?limit=50&q=group&linked_partitioning=1");

            var groups = new PagedResult<Group> { collection = new List<Group> { new Group() } };
            var response = new ApiResponse<PagedResult<Group>>(HttpStatusCode.OK, groups);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Group>>(expectedUri)).ReturnsAsync(response);

            // Act
            var builder = new GroupQueryBuilder { SearchString = "group" };
            var result = await new Groups(gatewayMock.Object).GetAsync(builder);

            // Assert
            Assert.That(result, Is.EqualTo(groups.collection));
        }

        [Test]
        public async Task GetMembers()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/members?limit=50&linked_partitioning=1");

            var members = new PagedResult<User> { collection = new List<User> { new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, members);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetMembersAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(members.collection));
        }

        [Test]
        public async Task GetModerators()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/moderators?limit=50&linked_partitioning=1");

            var moderators = new PagedResult<User> { collection = new List<User> { new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, moderators);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetModeratorsAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(moderators.collection));
        }

        [Test]
        public async Task GetPendingTracks()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/pending_tracks?limit=50&linked_partitioning=1");

            var tracks = new PagedResult<Track> { collection = new List<Track> { new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, tracks);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetPendingTracksAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(tracks.collection));
        }

        [Test]
        public async Task GetTracks()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/tracks?limit=50&linked_partitioning=1");

            var tracks = new PagedResult<Track> { collection = new List<Track> { new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, tracks);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetTracksAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(tracks.collection));
        }

        [Test]
        public async Task GetUsers()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/users?limit=50&linked_partitioning=1");

            var members = new PagedResult<User> { collection = new List<User> { new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, members);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var group = new Group { Id = GroupId };
            var result = await new Groups(gatewayMock.Object).GetUsersAsync(group);

            // Assert
            Assert.That(result, Is.EqualTo(members.collection));
        }

        [Test]
        public async Task Post()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups?");

            var group = new Group { name = "name" };
            var postedGroup = new Group { Id = 1, name = group.name };

            var response = new ApiResponse<Group>(HttpStatusCode.OK, postedGroup);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Group>(expectedUri, group)).ReturnsAsync(response);

            // Act
            var result = await new Groups(gatewayMock.Object).PostAsync(group);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedGroup));
        }

        [Test]
        public async Task Post_Track()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200/contributions?");

            var group = new Group { Id = GroupId };
            var track = new Track { Id = TrackId };

            var addedTrack = new Track { Id = track.Id };
            var response = new ApiResponse<Track>(HttpStatusCode.OK, addedTrack);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Track>(expectedUri, It.Is<Dictionary<string, object>>(y => y.ContainsValue(track.Id))))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p["track[id]"], Is.EqualTo(track.Id));
                });

            // Act
            var result = await new Groups(gatewayMock.Object).PostAsync(group, track);
            
            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(addedTrack));
        }

        [Test]
        public async Task Update()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200?");

            var group = new Group { Id = GroupId, name = "name" };
            var updatedGroup = new Group { Id = group.Id, name = group.name };

            var response = new ApiResponse<Group>(HttpStatusCode.OK, updatedGroup);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<Group>(expectedUri, group)).ReturnsAsync(response);

            // Act
            var result = await new Groups(gatewayMock.Object).UpdateAsync(group);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedGroup));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task UploadArtwork()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/groups/215200?");

            var group = new Group { Id = GroupId, name = "name" };
            var updatedGroup = new Group { Id = group.Id, name = group.name, artwork_url = "http://sampleurl.com" };

            var response = new ApiResponse<Group>(HttpStatusCode.OK, updatedGroup);

            var artwork = TestDataProvider.GetArtwork();

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<Group>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p["group[artwork_data]"], Is.EqualTo(artwork));
                });

            // Act
            var result = await new Groups(gatewayMock.Object).UploadArtworkAsync(group, artwork);
            artwork.Dispose();

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedGroup));
        }
    }
}