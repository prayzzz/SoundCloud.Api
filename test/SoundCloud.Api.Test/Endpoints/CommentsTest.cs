using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Utils;
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

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var comment = new Comment { Id = CommentId };
            var result = await commentEndpoint.DeleteAsync(comment);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Delete_Failure()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments/240653022?");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).Returns(Task.FromResult(response));

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var comment = new Comment { Id = CommentId };
            var result = await commentEndpoint.DeleteAsync(comment);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Get()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments/240653022?");

            var comment = new Comment();
            var response = new ApiResponse<Comment>(HttpStatusCode.OK, comment);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<Comment>(expectedUri)).Returns(Task.FromResult(response));

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var result = await commentEndpoint.GetAsync(CommentId);

            // Assert
            Assert.That(result, Is.EqualTo(comment));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task GetList()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments?limit=200&linked_partitioning=1");

            var comments = new PagedResult<Comment> { collection = new List<Comment> { new Comment(), new Comment() } };

            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, comments);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<Comment>>(expectedUri)).Returns(Task.FromResult(response));

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var result = (await commentEndpoint.GetAsync()).ToList();

            // Assert
            Assert.That(result, Is.EqualTo(comments.collection));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Post()
        {
            var expectedUri = new Uri("https://api.soundcloud.com/comments?");

            var comment = new Comment { body = "SampleComment", timestamp = 1000 };

            var response = new ApiResponse<Comment>(HttpStatusCode.Created, comment);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<Comment>(expectedUri, comment)).Returns(Task.FromResult(response));

            // Act
            var commentEndpoint = new Comments(gatewayMock.Object);
            var result = await commentEndpoint.PostAsync(comment);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(comment));

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