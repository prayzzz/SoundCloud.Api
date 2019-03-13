using System;
using System.Net;

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
        private const string ClientId = "myClientId";

        [Test]
        public void Test_Resolve_GetEntity()
        {
            const string requestedUrl = "https://soundcloud.com/sharpsound-2";
            const string expectedUri = @"https://api.soundcloud.com/resolve?url=https://soundcloud.com/sharpsound-2&client_id=myClientId";

            var user = new User();

            var response = new ApiResponse<Entity>(HttpStatusCode.OK, "OK");
            response.Data = user;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<Entity>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var resolveEndpoint = new Resolve(gatewayMock.Object);
            resolveEndpoint.Credentials.ClientId = ClientId;

            var result = resolveEndpoint.GetEntity(requestedUrl);

            Assert.That(result, Is.EqualTo(user));
        }
    }
}