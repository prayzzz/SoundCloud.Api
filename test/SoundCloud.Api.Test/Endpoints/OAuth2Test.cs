using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        public async Task ExchangeToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var accessRequest = new Credentials { client_id = "my client id", client_secret = "my client secret", code = "my code" };
            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, accessRequest);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["code"], Is.EqualTo("my code"));
                    Assert.That(p["grant_type"], Is.EqualTo("authorization_code"));
                });

            // Act
            var result = await new OAuth2(gatewayMock.Object).ExchangeTokenAsync(accessRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task Login()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var accessRequest = new Credentials
            {
                client_id = "my client id", client_secret = "my client secret", username = "my username", password = "my password"
            };
            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, accessRequest);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["username"], Is.EqualTo("my username"));
                    Assert.That(p["password"], Is.EqualTo("my password"));
                    Assert.That(p["grant_type"], Is.EqualTo("password"));
                });

            // Act
            var result = await new OAuth2(gatewayMock.Object).LoginAsync(accessRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task ClientCredentials()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var accessRequest = new Credentials { client_id = "my client id", client_secret = "my client secret" };
            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, accessRequest);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["grant_type"], Is.EqualTo("client_credentials"));
                });

            // Act
            var result = await new OAuth2(gatewayMock.Object).ClientCredentialsAsync(accessRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task RefreshToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var accessRequest = new Credentials
            {
                client_id = "my client id", client_secret = "my client secret", refresh_token = "my refresh token"
            };
            var response = new ApiResponse<Credentials>(HttpStatusCode.OK, accessRequest);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(response)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["refresh_token"], Is.EqualTo("my refresh token"));
                    Assert.That(p["grant_type"], Is.EqualTo("refresh_token"));
                });

            // Act
            var result = await new OAuth2(gatewayMock.Object).RefreshTokenAsync(accessRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<Credentials>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }
    }
}