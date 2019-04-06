using System;
using System.Collections.Generic;
using System.Linq;
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

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var app = new AppClient { Id = 123 };
            gatewayMock.Setup(x => x.SendGetRequestAsync<AppClient>(expectedUri)).ReturnsAsync(app);

            // Act
            var apps = new Apps(gatewayMock.Object);
            var result = await apps.GetAsync(123);

            // Assert
            Assert.That(result, Is.SameAs(app));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task GetApps()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/apps?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var appList = new PagedResult<AppClient> { Collection = new List<AppClient> { new AppClient(), new AppClient() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<AppClient>>(expectedUri)).ReturnsAsync(appList);

            // Act
            var apps = new Apps(gatewayMock.Object);
            var result = (await apps.GetAllAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(appList.Collection));

            gatewayMock.VerifyAll();
        }
    }
}