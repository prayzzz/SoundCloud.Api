using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AppsTest
    {
        [Test]
        public async Task GetApp()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/apps/123?");

            var app = new AppClient { Id = 123 };
            var response = new ApiResponse<AppClient>(HttpStatusCode.OK, app);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<AppClient>(expectedUri)).ReturnsAsync(response);

            // Act
            var apps = new Apps(gatewayMock.Object);
            var result = await apps.GetAsync(123);

            // Assert
            Assert.That(result, Is.EqualTo(app));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task GetApps()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/apps?limit=200&linked_partitioning=1");

            var appList = new PagedResult<AppClient> { Collection = new List<AppClient> { new AppClient(), new AppClient() } };
            var response = new ApiResponse<PagedResult<AppClient>>(HttpStatusCode.OK, appList);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<AppClient>>(expectedUri)).ReturnsAsync(response);

            // Act
            var apps = new Apps(gatewayMock.Object);
            var result = (await apps.GetAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(appList.Collection));

            gatewayMock.VerifyAll();
        }
    }
}