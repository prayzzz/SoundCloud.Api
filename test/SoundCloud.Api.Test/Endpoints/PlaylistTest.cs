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

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var playlist = new Playlist { Id = PlaylistId };
            var result = await new Playlists(requestMock.Object).DeleteAsync(playlist);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var playlist = new Playlist();
            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, playlist);

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeGetRequestAsync<Playlist>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Playlists(requestMock.Object).GetAsync(PlaylistId);

            // Assert
            Assert.That(result, Is.EqualTo(playlist));
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists?limit=10&q=search&representation=compact&linked_partitioning=1");

            var playlists = new PagedResult<Playlist> { collection = new List<Playlist> { new Playlist(), new Playlist() } };
            var response = new ApiResponse<PagedResult<Playlist>>(HttpStatusCode.OK, playlists);

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Playlist>>(expectedUri)).ReturnsAsync(response);

            // Act
            var builder = new PlaylistQueryBuilder("search") { Representation = RepresentationMode.Compact };
            var result = (await new Playlists(requestMock.Object).GetAsync(builder)).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(playlists.collection));
        }

        [Test]
        public async Task GetSecretToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739/secret-token?");

            var secretToken = new SecretToken();
            var response = new ApiResponse<SecretToken>(HttpStatusCode.OK, secretToken);

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeGetRequestAsync<SecretToken>(expectedUri)).ReturnsAsync(response);
            
            // Act
            var playlist = new Playlist { Id = PlaylistId };
            var result = await new Playlists(requestMock.Object).GetSecretTokenAsync(playlist);

            // Assert
            Assert.That(result, Is.EqualTo(secretToken));
        }

        [Test]
        public async Task Post()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists?");

            var playlist = new Playlist { Id = PlaylistId, title = "title", playlist_type = PlaylistType.Compilation };
            var postedPlaylist = new Playlist { Id = playlist.Id, title = playlist.title };
            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, postedPlaylist);

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeCreateRequestAsync<Playlist>(expectedUri, playlist)).ReturnsAsync(response);

            // Act
            var result = await new Playlists(requestMock.Object).PostAsync(playlist);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Playlist>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedPlaylist));
        }

        [Test]
        public async Task Update()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var playlist = new Playlist { Id = PlaylistId, title = "title" };
            var updatedPlaylist = new Playlist { Id = playlist.Id, title = playlist.title };
            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, updatedPlaylist);

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeUpdateRequestAsync<Playlist>(expectedUri, playlist)).ReturnsAsync(response);

            // Act
            var result = await new Playlists(requestMock.Object).UpdateAsync(playlist);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Playlist>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedPlaylist));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public async Task UploadArtwork()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/playlists/130208739?");

            var playlist = new Playlist { Id = PlaylistId, title = "title" };
            var updatedPlaylist = new Playlist { Id = playlist.Id, title = playlist.title, artwork_url = "http://sampleurl.com" };
            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, updatedPlaylist);

            var artwork = TestDataProvider.GetArtwork();

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeUpdateRequestAsync<Playlist>(expectedUri, It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync(response)
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
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Playlist>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedPlaylist));
        }
    }
}