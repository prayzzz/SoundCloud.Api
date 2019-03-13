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
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Test.Data;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class TrackTest
    {
        private const string ClientId = "myClientId";
        private const string Token = "myTokenId";
        private const int TrackId = 215850263;

        [Test]
        public void Test_Tracks_Delete()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263?oauth_token=myTokenId";

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.AccessToken = Token;

            var track = new Track();
            track.id = TrackId;

            var result = trackEndpoint.Delete(track);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Tracks_Get()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263?client_id=myClientId";

            var track = new Track();

            var response = new ApiResponse<Track>(HttpStatusCode.OK, "OK");
            response.Data = track;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<Track>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var result = trackEndpoint.Get(TrackId);

            Assert.That(result, Is.EqualTo(track));
        }

        [Test]
        public void Test_Tracks_Get_With_Bpm_And_Tag_Query()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks?limit=200&bpm[from]=100&tags=house&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var query = new TrackQueryBuilder();
            query.BpmFrom = 100;
            query.Tags.Add("house");

            var result = trackEndpoint.Get(query).ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Tracks_Get_With_Id_Query()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks?limit=200&ids=101%2C202%2C303&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var query = new TrackQueryBuilder();
            query.Ids.Add(101);
            query.Ids.Add(202);
            query.Ids.Add(303);

            var result = trackEndpoint.Get(query).ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Tracks_Get_With_License_Query()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks?limit=200&license=cc-by&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var query = new TrackQueryBuilder();
            query.License = License.CcBy;

            var result = trackEndpoint.Get(query).ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Tracks_Get_With_TrackType_And_Genre_Query()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks?limit=200&types=original&genres=Rap&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var query = new TrackQueryBuilder();
            query.TrackTypes.Add(TrackType.Original);
            query.Genres.Add("Rap");

            var result = trackEndpoint.Get(query).ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Tracks_GetComments()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263/comments?limit=200&linked_partitioning=1&client_id=myClientId";

            var commentList = new PagedResult<Comment>();
            commentList.collection = new List<Comment> {new Comment(), new Comment()};

            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, "OK");
            response.Data = commentList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Comment>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var track = new Track();
            track.id = TrackId;

            var result = trackEndpoint.GetComments(track).ToList();

            Assert.That(result, Is.EqualTo(commentList.collection));
        }

        [Test]
        public void Test_Tracks_GetFavoriters()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263/favoriters?limit=200&linked_partitioning=1&client_id=myClientId";

            var userList = new PagedResult<User>();
            userList.collection = new List<User> {new User(), new User()};

            var response = new ApiResponse<PagedResult<User>>(HttpStatusCode.OK, "OK");
            response.Data = userList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<User>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var track = new Track();
            track.id = TrackId;

            var result = trackEndpoint.GetFavoriters(track).ToList();

            Assert.That(result, Is.EqualTo(userList.collection));
        }

        [Test]
        public void Test_Tracks_GetList()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks?limit=200&linked_partitioning=1&client_id=myClientId";

            var trackList = new PagedResult<Track>();
            trackList.collection = new List<Track> {new Track(), new Track()};

            var response = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            response.Data = trackList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var result = trackEndpoint.Get().ToList();

            Assert.That(result, Is.EqualTo(trackList.collection));
        }

        [Test]
        public void Test_Tracks_GetSecretToken()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263/secret-token?oauth_token=myTokenId";

            var track = new Track();
            track.id = TrackId;
            track.title = "title";

            var token = new SecretToken();

            var response = new ApiResponse<SecretToken>(HttpStatusCode.OK, "OK");
            response.Data = token;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<SecretToken>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.AccessToken = Token;

            var result = trackEndpoint.GetSecretToken(track);

            Assert.That(result, Is.EqualTo(token));
        }

        [Test]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Test_Tracks_InfiniteScroll()
        {
            const string firstPageUri = @"https://api.soundcloud.com/tracks?limit=200&linked_partitioning=1&client_id=myClientId";
            const string secondPageuri = @"https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=50&client_id=myClientId";
            const string thirdPageUri = @"https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=100&client_id=myClientId";

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            var firstPage = new PagedResult<Track> {collection = new List<Track>()};
            firstPage.next_href = new Uri("https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=50");
            for (var i = 0; i < 50; i++)
            {
                firstPage.collection.Add(new Track());
            }

            var secondPage = new PagedResult<Track> {collection = new List<Track>()};
            secondPage.next_href = new Uri("https://api.soundcloud.com/tracks?linked_partitioning=1&limit=200&offset=100");
            for (var i = 0; i < 50; i++)
            {
                secondPage.collection.Add(new Track());
            }

            var thirdPage = new PagedResult<Track> {collection = new List<Track>()};
            for (var i = 0; i < 50; i++)
            {
                thirdPage.collection.Add(new Track());
            }

            var firstReponse = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            firstReponse.Data = firstPage;

            var secondResponse = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            secondResponse.Data = secondPage;

            var thirdResponse = new ApiResponse<PagedResult<Track>>(HttpStatusCode.OK, "OK");
            thirdResponse.Data = thirdPage;

            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == firstPageUri))).Returns(firstReponse);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == secondPageuri))).Returns(secondResponse);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Track>>(It.Is<Uri>(y => y.ToString() == thirdPageUri))).Returns(thirdResponse);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.ClientId = ClientId;

            var result = trackEndpoint.Get();

            var res1 = result.Take(50).ToList();
            var res2 = result.Skip(50).Take(50).ToList();
            var res3 = result.Skip(100).Take(50).ToList();

            Assert.That(res1, Is.EqualTo(firstPage.collection));
            Assert.That(res2, Is.EqualTo(secondPage.collection));
            Assert.That(res3, Is.EqualTo(thirdPage.collection));
        }

        [Test]
        public void Test_Tracks_Update()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263?oauth_token=myTokenId";

            var track = new Track();
            track.id = TrackId;
            track.title = "title";

            var updatedTrack = new Track();
            updatedTrack.id = track.id;
            updatedTrack.title = track.title;

            var response = new ApiResponse<Track>(HttpStatusCode.OK, "OK");
            response.Data = updatedTrack;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<Track>(It.Is<Uri>(y => y.ToString() == expectedUri), track)).Returns(response);

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.AccessToken = Token;

            var result = trackEndpoint.Update(track);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Track>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedTrack));
        }

        [Test]
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public void Test_Tracks_UploadArtwork()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks/215850263?oauth_token=myTokenId";

            var track = new Track();
            track.id = TrackId;
            track.title = "title";

            var updatedTrack = new Track();
            updatedTrack.id = track.id;
            updatedTrack.title = track.title;
            updatedTrack.artwork_url = new Uri("http://sampleurl.com");

            var artwork = TestDataProvider.GetArtwork();

            var response = new ApiResponse<Track>(HttpStatusCode.OK, "OK");
            response.Data = updatedTrack;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<Track>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<Dictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(1));
                    Assert.That(p["track[artwork_data]"], Is.EqualTo(artwork));
                });

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.AccessToken = Token;

            var result = trackEndpoint.UploadArtwork(track, artwork);
            artwork.Dispose();

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Track>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(updatedTrack));
        }

        [Test]
        public void Test_Tracks_UploadTrack()
        {
            const string expectedUri = @"https://api.soundcloud.com/tracks?oauth_token=myTokenId";

            var postedTrack = new Track();
            postedTrack.id = TrackId;
            postedTrack.title = "title";

            var sound = TestDataProvider.GetSound();

            var response = new ApiResponse<Track>(HttpStatusCode.OK, "OK");
            response.Data = postedTrack;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Track>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<Dictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p.Count, Is.EqualTo(3));
                    Assert.That(p["oauth_token"], Is.EqualTo(Token));
                    Assert.That(p["track[title]"], Is.EqualTo("title"));
                });

            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.AccessToken = Token;

            var result = trackEndpoint.UploadTrack("title", sound);
            sound.Dispose();

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Track>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(postedTrack));
        }

        [Test]
        public void Test_Tracks_UploadTrack_Fail()
        {
            var sound = TestDataProvider.GetSound();

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            var trackEndpoint = new Tracks(gatewayMock.Object);
            trackEndpoint.Credentials.AccessToken = Token;

            Assert.Throws<SoundCloudValidationException>(() => trackEndpoint.UploadTrack(string.Empty, sound));

            sound.Dispose();
        }
    }
}