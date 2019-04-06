using System;
using System.Collections.Generic;
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

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var accessRequest = new Credentials { ClientId = "my client id", ClientSecret = "my client secret", Code = "my code" };

            gatewayMock.Setup(x => x.SendPostRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(accessRequest)
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
            Assert.That(result, Is.SameAs(accessRequest));
        }

        [Test]
        public async Task Login()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var accessRequest = new Credentials
            {
                ClientId = "my client id", ClientSecret = "my client secret", Username = "my username", Password = "my password"
            };
            gatewayMock.Setup(x => x.SendPostRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(accessRequest)
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
            Assert.That(result, Is.SameAs(accessRequest));
        }

        [Test]
        public async Task ClientCredentials()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var accessRequest = new Credentials { ClientId = "my client id", ClientSecret = "my client secret" };

            gatewayMock.Setup(x => x.SendPostRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                .ReturnsAsync(accessRequest)
                .Callback((Uri u, IDictionary<string, object> p) =>
                {
                    Assert.That(p["client_id"], Is.EqualTo("my client id"));
                    Assert.That(p["client_secret"], Is.EqualTo("my client secret"));
                    Assert.That(p["grant_type"], Is.EqualTo("client_credentials"));
                });

            // Act
            var result = await new OAuth2(gatewayMock.Object).ClientCredentialsAsync(accessRequest);

            // Assert
            Assert.That(result, Is.SameAs(accessRequest));
        }

        [Test]
        public async Task RefreshToken()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/oauth2/token?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var accessRequest = new Credentials
            {
                ClientId = "my client id", ClientSecret = "my client secret", RefreshToken = "my refresh token"
            };
            gatewayMock.Setup(x => x.SendPostRequestAsync<Credentials>(expectedUri, It.IsAny<IDictionary<string, object>>()))
                       .ReturnsAsync(accessRequest)
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
            Assert.That(result, Is.SameAs(accessRequest));
        }
    }
}