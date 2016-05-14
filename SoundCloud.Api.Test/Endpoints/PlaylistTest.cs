using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;

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
        private const string ClientId = "myClientId";
        private const int PlaylistId = 130208739;
        private const string Token = "myTokenId";

        [Test]
        public void Test_Playlists_Delete()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists/130208739?oauth_token=myTokenId";

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.AccessToken = Token;

            var playlist = new Playlist();
            playlist.id = PlaylistId;

            var result = playlistEndpoint.Delete(playlist);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Playlists_Get()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists/130208739?client_id=myClientId";

            var playlist = new Playlist();

            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, "OK");
            response.Data = playlist;

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeGetRequest<Playlist>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.ClientId = ClientId;

            var result = playlistEndpoint.Get(PlaylistId);

            Assert.That(result, Is.EqualTo(playlist));
        }

        [Test]
        public void Test_Playlists_GetList()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists?limit=10&q=search&representation=compact&linked_partitioning=1&client_id=myClientId";

            var playlists = new PagedResult<Playlist>();
            playlists.collection = new List<Playlist> {new Playlist(), new Playlist()};

            var response = new ApiResponse<PagedResult<Playlist>>(HttpStatusCode.OK, "OK");
            response.Data = playlists;

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeGetRequest<PagedResult<Playlist>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.ClientId = ClientId;

            var builder = new PlaylistQueryBuilder("search");
            builder.Representation = RepresentationMode.Compact;

            var result = playlistEndpoint.Get(builder).ToList();

            Assert.That(result, Is.EqualTo(playlists.collection));
        }

        [Test]
        public void Test_Playlists_GetSecretToken()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists/130208739/secret-token?oauth_token=myTokenId";

            var secretToken = new SecretToken();

            var response = new ApiResponse<SecretToken>(HttpStatusCode.OK, "OK");
            response.Data = secretToken;

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeGetRequest<SecretToken>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.AccessToken = Token;

            var playlist = new Playlist();
            playlist.id = PlaylistId;

            var result = playlistEndpoint.GetSecretToken(playlist);

            Assert.That(result, Is.EqualTo(secretToken));
        }

        [Test]
        public void Test_Playlists_Post()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists?oauth_token=myTokenId";

            var playlist = new Playlist();
            playlist.id = PlaylistId;
            playlist.title = "title";

            var postedPlaylist = new Playlist();
            postedPlaylist.id = playlist.id;
            postedPlaylist.title = playlist.title;

            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, "OK");
            response.Data = postedPlaylist;

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeCreateRequest<Playlist>(It.Is<Uri>(y => y.ToString() == expectedUri), playlist)).Returns(response);

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.AccessToken = Token;

            var result = playlistEndpoint.Post(playlist);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Playlist>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedPlaylist));
        }

        [Test]
        public void Test_Playlists_Update()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists/130208739?oauth_token=myTokenId";

            var playlist = new Playlist();
            playlist.id = PlaylistId;
            playlist.title = "title";

            var updatedPlaylist = new Playlist();
            updatedPlaylist.id = playlist.id;
            updatedPlaylist.title = playlist.title;

            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, "OK");
            response.Data = updatedPlaylist;

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeUpdateRequest<Playlist>(It.Is<Uri>(y => y.ToString() == expectedUri), playlist)).Returns(response);

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.AccessToken = Token;

            var result = playlistEndpoint.Update(playlist);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Playlist>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedPlaylist));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public void Test_Playlists_UploadArtwork()
        {
            const string expectedUri = @"https://api.soundcloud.com/playlists/130208739?oauth_token=myTokenId";

            var playlist = new Playlist();
            playlist.id = PlaylistId;
            playlist.title = "title";

            var updatedPlaylist = new Playlist();
            updatedPlaylist.id = playlist.id;
            updatedPlaylist.title = playlist.title;
            updatedPlaylist.artwork_url = "http://sampleurl.com";

            var response = new ApiResponse<Playlist>(HttpStatusCode.OK, "OK");
            response.Data = updatedPlaylist;

            var artwork = TestDataProvider.GetArtwork();

            var requestMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            requestMock.Setup(x => x.InvokeUpdateRequest<Playlist>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<Dictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p.First().Key, Is.EqualTo("playlist[artwork_data]"));
                    Assert.That(p.First().Value, Is.EqualTo(artwork));
                });

            var playlistEndpoint = new Playlists(requestMock.Object);
            playlistEndpoint.Credentials.AccessToken = Token;

            var result = playlistEndpoint.UploadArtwork(playlist, artwork);
            artwork.Dispose();

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Playlist>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedPlaylist));
        }
    }
}