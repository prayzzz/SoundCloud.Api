using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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
        private const string ClientId = "myClientId";
        private const int CommentId = 240653022;
        private const string Token = "myTokenId";

        [Test]
        public void Test_Comments_Delete()
        {
            const string expectedUri = @"https://api.soundcloud.com/comments/240653022?oauth_token=myTokenId";

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var commentEndpoint = new Comments(gatewayMock.Object);
            commentEndpoint.Credentials.AccessToken = Token;

            var comment = new Comment();
            comment.id = CommentId;

            var result = commentEndpoint.Delete(comment);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Comments_Delete_Failure()
        {
            const string expectedUri = @"https://api.soundcloud.com/comments/240653022?oauth_token=myTokenId";

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, "Not Found");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var commentEndpoint = new Comments(gatewayMock.Object);
            commentEndpoint.Credentials.AccessToken = Token;

            var comment = new Comment();
            comment.id = CommentId;

            var result = commentEndpoint.Delete(comment);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Comments_Get()
        {
            const string expectedUri = @"https://api.soundcloud.com/comments/240653022?client_id=myClientId";

            var comment = new Comment();
            var response = new ApiResponse<Comment>(HttpStatusCode.OK, "OK");
            response.Data = comment;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<Comment>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var commentEndpoint = new Comments(gatewayMock.Object);
            commentEndpoint.Credentials.ClientId = ClientId;

            var result = commentEndpoint.Get(CommentId);

            Assert.That(result, Is.EqualTo(comment));
        }

        [Test]
        public void Test_Comments_GetList()
        {
            const string expectedUri = @"https://api.soundcloud.com/comments?limit=200&linked_partitioning=1&client_id=myClientId";

            var comments = new PagedResult<Comment>();
            comments.collection = new List<Comment> {new Comment(), new Comment()};

            var response = new ApiResponse<PagedResult<Comment>>(HttpStatusCode.OK, "OK");
            response.Data = comments;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<Comment>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var commentEndpoint = new Comments(gatewayMock.Object);
            commentEndpoint.Credentials.ClientId = ClientId;

            var result = commentEndpoint.Get().ToList();

            Assert.That(result, Is.EqualTo(comments.collection));
        }

        [Test]
        public void Test_Comments_Post()
        {
            const string expectedUri = @"https://api.soundcloud.com/comments?oauth_token=myTokenId";

            var comment = new Comment();
            comment.body = "SampleComment";
            comment.timestamp = 1000;

            var response = new ApiResponse<Comment>(HttpStatusCode.Created, "Created");
            response.Data = comment;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<Comment>(It.Is<Uri>(y => y.ToString() == expectedUri), comment)).Returns(response);

            var commentEndpoint = new Comments(gatewayMock.Object);
            commentEndpoint.Credentials.AccessToken = Token;

            var result = commentEndpoint.Post(comment);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
            Assert.That(result.Data, Is.EqualTo(comment));
        }

        [Test]
        public void Test_Comments_Post_Invalid()
        {
            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            var commentEndpoint = new Comments(gatewayMock.Object);
            commentEndpoint.Credentials.AccessToken = Token;

            var comment = new Comment();
            Assert.Throws<SoundCloudValidationException>(() => commentEndpoint.Post(comment));
        }
    }
}