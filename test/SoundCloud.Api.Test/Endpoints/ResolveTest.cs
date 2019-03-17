using System;
using System.Net;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class ResolveTest
    {
        [Test]
        public async Task GetEntity()
        {
            const string requestedUrl = "https://soundcloud.com/sharpsound-2";
            var expectedUri = new Uri("https://api.soundcloud.com/resolve?url=https://soundcloud.com/sharpsound-2");

            var user = new User();
            var response = new ApiResponse<Entity>(HttpStatusCode.OK, user);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<Entity>(expectedUri)).ReturnsAsync(response);

            // Act
            var result = await new Resolve(gatewayMock.Object).GetEntityAsync(requestedUrl);

            // Assert
            Assert.That(result, Is.EqualTo(user));
        }
    }
}