using System;
using System.Collections.Generic;
using System.Net;

using Moq;

using NUnit.Framework;

using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class OAuth2Test
    {
        [Test]
        public void Test_OAuth2_ExchangeToken()
        {
            const string expectedUri = @"https://api.soundcloud.com/oauth2/token?";

            var accessRequest = new Credentials();
            accessRequest.client_id = "my client id";
            accessRequest.client_secret = "my client secret";
            accessRequest.code = "my code";

            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, "OK");
            response.Data = accessRequest;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Credentials>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<IDictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["code"], Is.EqualTo("my code"));
                    Assert.That(p["grant_type"], Is.EqualTo("authorization_code"));
                });

            var oauth2Endpoint = new OAuth2(gatewayMock.Object);

            var result = oauth2Endpoint.ExchangeToken(accessRequest);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_OAuth2_Login()
        {
            const string expectedUri = @"https://api.soundcloud.com/oauth2/token?";

            var accessRequest = new Credentials();
            accessRequest.client_id = "my client id";
            accessRequest.client_secret = "my client secret";
            accessRequest.username = "my username";
            accessRequest.password = "my password";

            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, "OK");
            response.Data = accessRequest;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Credentials>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<IDictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["username"], Is.EqualTo("my username"));
                    Assert.That(p["password"], Is.EqualTo("my password"));
                    Assert.That(p["grant_type"], Is.EqualTo("client_credentials"));
                });

            var oauth2Endpoint = new OAuth2(gatewayMock.Object);

            var result = oauth2Endpoint.Login(accessRequest);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_OAuth2_RefreshToken()
        {
            const string expectedUri = @"https://api.soundcloud.com/oauth2/token?";

            var accessRequest = new Credentials();
            accessRequest.client_id = "my client id";
            accessRequest.client_secret = "my client secret";
            accessRequest.refresh_token = "my refresh token";

            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, "OK");
            response.Data = accessRequest;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Credentials>(It.Is<Uri>(y => y.ToString() == expectedUri), It.IsAny<IDictionary<string, object>>()))
                .Returns(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["refresh_token"], Is.EqualTo("my refresh token"));
                    Assert.That(p["grant_type"], Is.EqualTo("refresh_token"));
                });

            var oauth2Endpoint = new OAuth2(gatewayMock.Object);

            var result = oauth2Endpoint.RefreshToken(accessRequest);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }
    }
}