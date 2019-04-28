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
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Test.Data;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class PlaylistTest
    {
        private const int PlaylistId = 130208739;

        [Test]
        public async Task Delete()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var playlist = new Playlist { Id = PlaylistId };
            var result = await new Playlists(gatewayMock.Object).DeleteAsync(playlist);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlist = new Playlist();
            requestMock.Setup(x => x.SendGetRequestAsync<Playlist>(expectedUri)).ReturnsAsync(playlist);

            // Act
            var result = await new Playlists(requestMock.Object).GetAsync(PlaylistId);

            // Assert
            Assert.That(result, Is.SameAs(playlist));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists?limit=200&offset=0&q=search&linked_partitioning=1");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlists = new PagedResult<Playlist> { Collection = new List<Playlist> { new Playlist(), new Playlist() } };
            requestMock.Setup(x => x.SendGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(playlists);

            // Act
            var result = (await new Playlists(requestMock.Object).GetAllAsync("search")).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(playlists.Collection));
        }

        [Test]
        public async Task GetListWithBuilder()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists?limit=10&offset=0&q=search&representation=compact&linked_partitioning=1");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlists = new PagedResult<Playlist> { Collection = new List<Playlist> { new Playlist(), new Playlist() } };
            requestMock.Setup(x => x.SendGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(playlists);

            // Act
            var builder = new PlaylistQueryBuilder("search") { Representation = RepresentationMode.Compact };
            var result = (await new Playlists(requestMock.Object).GetAllAsync(builder)).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(playlists.Collection));
        }

        [Test]
        public async Task GetSecretToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739/secret-token?");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var secretToken = new SecretToken();
            requestMock.Setup(x => x.SendGetRequestAsync<SecretToken>(expectedUri)).ReturnsAsync(secretToken);

            // Act
            var playlist = new Playlist { Id = PlaylistId };
            var result = await new Playlists(requestMock.Object).GetSecretTokenAsync(playlist);

            // Assert
            Assert.That(result, Is.SameAs(secretToken));
        }

        [Test]
        public async Task Post()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists?");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlist = new Playlist { Id = PlaylistId, Title = "title", PlaylistType = PlaylistType.Compilation };
            var postedPlaylist = new Playlist { Id = playlist.Id, Title = playlist.Title };
            requestMock.Setup(x => x.SendPostRequestAsync<Playlist>(expectedUri, playlist)).ReturnsAsync(postedPlaylist);

            // Act
            var result = await new Playlists(requestMock.Object).PostAsync(playlist);

            // Assert
            Assert.That(result, Is.SameAs(postedPlaylist));
        }

        [Test]
        public async Task Update()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var playlist = new Playlist { Id = PlaylistId, Title = "title" };
            var updatedPlaylist = new Playlist { Id = playlist.Id, Title = playlist.Title };
            requestMock.Setup(x => x.SendPutRequestAsync<Playlist>(expectedUri, playlist)).ReturnsAsync(updatedPlaylist);

            // Act
            var result = await new Playlists(requestMock.Object).UpdateAsync(playlist);

            // Assert
            Assert.That(result, Is.SameAs(updatedPlaylist));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task UploadArtwork()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var artwork = TestDataProvider.GetArtwork();
            var playlist = new Playlist { Id = PlaylistId, Title = "title" };
            var updatedPlaylist = new Playlist { Id = playlist.Id, Title = playlist.Title, ArtworkUrl = "http://sampleurl.com" };

            requestMock.Setup(x => x.SendPutRequestAsync<Playlist>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                       .ReturnsAsync(updatedPlaylist)
                       .Callback((Uri u, IDictionary<string, object> p) =>
                       {
                           Assert.That(p.Count, Is.EqualTo(1));
                           Assert.That(p.First().Key, Is.EqualTo("playlist[artwork_data]"));
                           Assert.That(p.First().Value, Is.EqualTo(artwork));
                       });

            // Act
            var result = await new Playlists(requestMock.Object).UploadArtworkAsync(playlist, artwork);
            artwork.Dispose();

            // Assert
            Assert.That(result, Is.SameAs(updatedPlaylist));
        }
    }
}