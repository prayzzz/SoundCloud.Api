using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class CommentsTest
    {
        private const int CommentId = 240653022;

        [Test]
        public async Task Delete()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments/240653022?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var statusResponse = new StatusResponse();
            gatewayMock.Setup(x => x.SendDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(statusResponse);

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var comment = new Comment { Id = CommentId };
            var result = await commentEndpoint.DeleteAsync(comment);

            // Assert
            Assert.That(result, Is.SameAs(statusResponse));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments/240653022?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var comment = new Comment();
            gatewayMock.Setup(x => x.SendGetRequestAsync<Comment>(expectedUri)).ReturnsAsync(comment);

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var result = await commentEndpoint.GetAsync(CommentId);

            // Assert
            Assert.That(result, Is.SameAs(comment));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments/?limit=200&linked_partitioning=1");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var comments = new PagedResult<Comment> { Collection = new List<Comment> { new Comment(), new Comment() } };
            gatewayMock.Setup(x => x.SendGetRequestAsync<PagedResult<Comment>>(expectedUri)).ReturnsAsync(comments);

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var result = (await commentEndpoint.GetAsync()).ToList();

            // Assert
            Assert.That(result, Is.EquivalentTo(comments.Collection));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Post()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments/?");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            var comment = new Comment { Body = "SampleComment", Timestamp = 1000 };
            gatewayMock.Setup(x => x.SendPostRequestAsync<Comment>(expectedUri, comment)).ReturnsAsync(comment);

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var result = await commentEndpoint.PostAsync(comment);

            // Assert
            Assert.That(result, Is.SameAs(comment));

            gatewayMock.VerifyAll();
        }

        [Test]
        public void Post_Invalid()
        {
            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            Assert.ThrowsAsync<SoundCloudValidationException>(() => commentEndpoint.PostAsync(new Comment()));

            gatewayMock.VerifyAll();
        }
    }
}