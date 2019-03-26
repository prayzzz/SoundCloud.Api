using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Test.Data;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class TrackTest
    {
        private const int TrackId = 215850263;

        [Test]
        public async Task Delete()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var track = new Track { Id = TrackId };
            var result = await new Tracks(gatewayMock.Object).DeleteAsync(track);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var track = new Track();
            var response = new ApiResponse<Track>(HttpStatusCode.OK, track);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<Track>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Tracks(gatewayMock.Object).GetAsync(TrackId);

            // Assert
            Assert.That(result, Is.EqualTo(track));
        }

        [Test]
        public async Task Get_With_Bpm_And_Tag_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&bpm[from]=100&tags=house&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var query = new TrackQueryBuilder { BpmFrom = 100, Tags = { "house" } };
            var result = (await new Tracks(gatewayMock.Object).GetAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task Get_With_Id_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&ids=101%2C202%2C303&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var query = new TrackQueryBuilder { Ids = { 101, 202, 303 } };
            var result = (await new Tracks(gatewayMock.Object).GetAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task Get_With_License_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&license=cc-by&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var query = new TrackQueryBuilder { License = License.CcBy };
            var result = (await new Tracks(gatewayMock.Object).GetAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task Get_With_TrackType_And_Genre_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&types=original&genres=Rap&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var query = new TrackQueryBuilder { TrackTypes = { TrackType.Original }, Genres = { "Rap" } };
            var result = (await new Tracks(gatewayMock.Object).GetAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task GetComments()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263/comments?limit=200&linked_partitioning=1");

            var commentList = new PagedResult<Comment> { Collection = new List<Comment> { new Comment(), new Comment() } };
            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, commentList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(response);

            // Act
            var track = new Track { Id = TrackId };
            var result = (await new Tracks(gatewayMock.Object).GetCommentsAsync(track)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(commentList.Collection));
        }

        [Test]
        public async Task GetFavoriters()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263/favoriters?limit=200&linked_partitioning=1");

            var userList = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, userList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(response);

            // Act
            var track = new Track { Id = TrackId };
            var result = (await new Tracks(gatewayMock.Object).GetFavoritersAsync(track)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(userList.Collection));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&linked_partitioning=1");

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, trackList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = (await new Tracks(gatewayMock.Object).GetAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(trackList.Collection));
        }

        [Test]
        public async Task GetSecretToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263/secret-token?");

            var track = new Track { Id = TrackId, Title = "title" };
            var token = new SecretToken();

            var response = new ApiResponse<SecretToken>(HttpStatusCode.OK, token);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<SecretToken>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Tracks(gatewayMock.Object).GetSecretTokenAsync(track);

            // Assert
            Assert.That(result, Is.EqualTo(token));
        }

        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public async Task InfiniteScroll()
        {
            const string firstPageUri = @"https://api.soundcloud.com/tracks?limit=200&linked_partitioning=1";
            const string secondPageuri = @"https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=50";
            const string thirdPageUri = @"https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=100";

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            var firstPage = new PagedResult<Track> { Collection = new List<Track>() };
            firstPage.NextHref = new Uri("https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=50");
            for (var i = 0; i < 50; i++)
            {
                firstPage.Collection.Add(new Track());
            }

            var secondPage = new PagedResult<Track> { Collection = new List<Track>() };
            secondPage.NextHref = new Uri("https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=100");
            for (var i = 0; i < 50; i++)
            {
                secondPage.Collection.Add(new Track());
            }

            var thirdPage = new PagedResult<Track> { Collection = new List<Track>() };
            for (var i = 0; i < 50; i++)
            {
                thirdPage.Collection.Add(new Track());
            }

            var firstReponse = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK);
            firstReponse.Data = firstPage;

            var secondResponse = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK);
            secondResponse.Data = secondPage;

            var thirdResponse = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK);
            thirdResponse.Data = thirdPage;

            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == firstPageUri)))
                .ReturnsAsync(firstReponse);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == secondPageuri)))
                .ReturnsAsync(secondResponse);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == thirdPageUri)))
                .ReturnsAsync(thirdResponse);

            // Act
            var result = await new Tracks(gatewayMock.Object).GetAsync();

            // Assert
            var res1 = result.Take(50).ToList();
            var res2 = result.Skip(50).Take(50).ToList();
            var res3 = result.Skip(100).Take(50).ToList();

            Assert.That(res1, Is.EqualTo(firstPage.Collection));
            Assert.That(res2, Is.EqualTo(secondPage.Collection));
            Assert.That(res3, Is.EqualTo(thirdPage.Collection));
        }

        [Test]
        public async Task Update()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var track = new Track { Id = TrackId, Title = "title" };
            var updatedTrack = new Track { Id = track.Id, Title = track.Title };

            var response = new ApiResponse<Track>(HttpStatusCode.OK, updatedTrack);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<Track>(expectedUri, track)).ReturnsAsync(response);

            // Act
            var result = await new Tracks(gatewayMock.Object).UpdateAsync(track);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Track>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedTrack));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task UploadArtwork()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var track = new Track { Id = TrackId, Title = "title" };
            var updatedTrack = new Track { Id = track.Id, Title = track.Title, ArtworkUrl = new Uri("http://sampleurl.com") };
            var artwork = TestDataProvider.GetArtwork();

            var response = new ApiResponse<Track>(HttpStatusCode.OK, updatedTrack);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<Track>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p["track[artwork_data]"], Is.EqualTo(artwork));
                });

            // Act
            var result = await new Tracks(gatewayMock.Object).UploadArtworkAsync(track, artwork);
            artwork.Dispose();

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Track>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedTrack));
        }

        [Test]
        public async Task UploadTrack()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?");

            var postedTrack = new Track { Id = TrackId, Title = "title" };
            var response = new ApiResponse<Track>(HttpStatusCode.OK, postedTrack);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Track>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(2));
                    Assert.That(p["track[title]"], Is.EqualTo("title"));
                });

            // Act
            var sound = TestDataProvider.GetSound();
            var result = await new Tracks(gatewayMock.Object).UploadTrackAsync("title", sound);
            sound.Dispose();

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Track>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedTrack));
        }

        [Test]
        public void UploadTrack_Fail()
        {
            var sound = TestDataProvider.GetSound();

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            // Act
            var trackEndpoint = new Tracks(gatewayMock.Object);
            Assert.ThrowsAsync<SoundCloudValidationException>(() => trackEndpoint.UploadTrackAsync(string.Empty, sound));

            sound.Dispose();
        }
    }
}