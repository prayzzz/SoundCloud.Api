using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var track = new Track { Id = TrackId };
            var result = await new Tracks(gatewayMock.Object).DeleteAsync(track);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var track = new Track();
            gatewayMock.Setup(x => x.SendGetRequestAsync<Track>(expectedUri)).ReturnsAsync(track);

            // Act
            var result = await new Tracks(gatewayMock.Object).GetAsync(TrackId);

            // Assert
            Assert.That(result, Is.SameAs(track));
        }

        [Test]
        public async Task Get_With_Bpm_And_Tag_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&offset=0&bpm[from]=100&tags=house&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var query = new TrackQueryBuilder { BpmFrom = 100, Tags = { "house" } };
            var result = (await new Tracks(gatewayMock.Object).GetAllAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task Get_With_Id_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&offset=0&ids=101%2C202%2C303&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var query = new TrackQueryBuilder { Ids = { 101, 202, 303 } };
            var result = (await new Tracks(gatewayMock.Object).GetAllAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task Get_With_License_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&offset=0&license=cc-by&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var query = new TrackQueryBuilder { License = License.CcBy };
            var result = (await new Tracks(gatewayMock.Object).GetAllAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task Get_With_TrackType_And_Genre_Query()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&offset=0&types=original&genres=Rap&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var query = new TrackQueryBuilder { TrackTypes = { TrackType.Original }, Genres = { "Rap" } };
            var result = (await new Tracks(gatewayMock.Object).GetAllAsync(query)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task GetComments()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263/comments?limit=200&offset=0&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var commentList = new PagedResult<Comment> { Collection = new List<Comment> { new Comment(), new Comment() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(commentList);

            // Act
            var track = new Track { Id = TrackId };
            var result = (await new Tracks(gatewayMock.Object).GetCommentsAsync(track)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(commentList.Collection));
        }

        [Test]
        public async Task GetFavoriters()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263/favoriters?limit=200&offset=0&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var userList = new PagedResult<User> { Collection = new List<User> { new User(), new User() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<User>>(expectedUri)).ReturnsAsync(userList);

            // Act
            var track = new Track { Id = TrackId };
            var result = (await new Tracks(gatewayMock.Object).GetFavoritersAsync(track)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(userList.Collection));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?limit=200&offset=0&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var trackList = new PagedResult<Track> { Collection = { new Track(), new Track() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Track>>(expectedUri)).ReturnsAsync(trackList);

            // Act
            var result = (await new Tracks(gatewayMock.Object).GetAllAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(trackList.Collection));
        }

        [Test]
        public async Task GetSecretToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263/secret-token?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var track = new Track { Id = TrackId, Title = "title" };
            var token = new SecretToken();
            gatewayMock.Setup(x => x.SendGetRequestAsync<SecretToken>(expectedUri)).ReturnsAsync(token);

            // Act
            var result = await new Tracks(gatewayMock.Object).GetSecretTokenAsync(track);

            // Assert
            Assert.That(result, Is.SameAs(token));
        }

        [Test]
        public async Task Update()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var track = new Track { Id = TrackId, Title = "title" };
            var updatedTrack = new Track { Id = track.Id, Title = track.Title };
            gatewayMock.Setup(x => x.SendPutRequestAsync<Track>(expectedUri, track)).ReturnsAsync(updatedTrack);

            // Act
            var result = await new Tracks(gatewayMock.Object).UpdateAsync(track);

            // Assert
            Assert.That(result, Is.SameAs(updatedTrack));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task UploadArtwork()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks/215850263?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var artwork = TestDataProvider.GetArtwork();
            var track = new Track { Id = TrackId, Title = "title" };
            var updatedTrack = new Track { Id = track.Id, Title = track.Title, ArtworkUrl = new Uri("http://sampleurl.com") };

            gatewayMock.Setup(x => x.SendPutRequestAsync<Track>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                       .ReturnsAsync(updatedTrack)
                       .Callback((Uri u, IDictionary<string, object> p) =>
                       {
                           Assert.That(p.Count, Is.EqualTo(1));
                           Assert.That(p["track[artwork_data]"], Is.EqualTo(artwork));
                       });

            // Act
            var result = await new Tracks(gatewayMock.Object).UploadArtworkAsync(track, artwork);
            artwork.Dispose();

            // Assert
            Assert.That(result, Is.SameAs(updatedTrack));
        }

        [Test]
        public async Task UploadTrack()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/tracks?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var postedTrack = new Track { Id = TrackId, Title = "title" };
            gatewayMock.Setup(x => x.SendPostRequestAsync<Track>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                       .ReturnsAsync(postedTrack)
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
            Assert.That(result, Is.SameAs(postedTrack));
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