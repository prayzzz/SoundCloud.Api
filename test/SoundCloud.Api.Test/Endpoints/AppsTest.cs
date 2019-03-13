using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Moq;

using NUnit.Framework;

using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class AppsTest
    {
        private const string ClientId = "myClientId";

        [Test]
        public void Test_Apps_GetApp()
        {
            const string expectedUri = @"https://api.soundcloud.com/apps/123?client_id=myClientId";

            var app = new AppClient();
            app.id = 123;

            var response = new ApiResponse<AppClient>(HttpStatusCode.OK, "OK");
            response.Data = app;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<AppClient>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var apps = new Apps(gatewayMock.Object);
            apps.Credentials.ClientId = ClientId;

            var result = apps.Get(123);

            Assert.That(result, Is.EqualTo(app));
        }

        [Test]
        public void Test_Apps_GetApps()
        {
            const string expectedUri = @"https://api.soundcloud.com/apps?limit=200&linked_partitioning=1&client_id=myClientId";

            var appList = new PagedResult<AppClient>();
            appList.collection = new List<AppClient> {new AppClient(), new AppClient()};

            var response = new ApiResponse<PagedResult<AppClient>>(HttpStatusCode.OK, "OK");
            response.Data = appList;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<AppClient>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var apps = new Apps(gatewayMock.Object);
            apps.Credentials.ClientId = ClientId;

            var result = apps.Get().ToList();

            Assert.That(result, Is.EqualTo(appList.collection));
        }
    }
}