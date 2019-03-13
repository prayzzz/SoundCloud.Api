using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Moq;

using NUnit.Framework;

using SoundCloud.Api.Endpoints;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Base;
using SoundCloud.Api.Utils;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Endpoints
{
    [TestFixture]
    public class EndpointTest
    {
        [Test]
        public void Test_Create_Entity_Fail()
        {
            const string expectedUri = @"https://test.com/create";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, "Not Found");
            response.Data = testEntity;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), testEntity)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Create<TestEntity>(expectedUri, testEntity);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Create_Entity_Success()
        {
            const string expectedUri = @"https://test.com/create";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");
            response.Data = testEntity;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), testEntity)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Create<TestEntity>(expectedUri, testEntity);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Create_Entity_Success_No_Data()
        {
            const string expectedUri = @"https://test.com/create";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), testEntity)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Create<TestEntity>(expectedUri, testEntity);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Create_Params_Fail()
        {
            const string expectedUri = @"https://test.com/create";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, "Not Found");
            response.Data = testEntity;

            var parameters = new Dictionary<string, object>();
            parameters.Add("foo", "bar");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), parameters)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Create<TestEntity>(expectedUri, parameters);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Create_Params_Success()
        {
            const string expectedUri = @"https://test.com/create";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");
            response.Data = testEntity;

            var parameters = new Dictionary<string, object>();
            parameters.Add("foo", "bar");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), parameters)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Create<TestEntity>(expectedUri, parameters);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Create_Params_Success_No_Data()
        {
            const string expectedUri = @"https://test.com/create";

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");

            var parameters = new Dictionary<string, object>();
            parameters.Add("foo", "bar");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), parameters)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Create<TestEntity>(expectedUri, parameters);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Delete_Fail()
        {
            const string expectedUri = @"https://test.com/delete";

            var status = new StatusResponse();
            status.Error = "FooError";
            status.Errors.Add(new ErrorMessage {error_message = "BarError"});

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, "Not Found");
            response.Data = status;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Delete(expectedUri);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("FooError\r\nBarError"));
        }

        [Test]
        public void Test_Delete_Success()
        {
            const string expectedUri = @"https://test.com/delete";

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Delete(expectedUri);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Get_Fail()
        {
            const string expectedUri = @"https://test.com/get";

            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, "Not Found");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.GetById<TestEntity>(expectedUri);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Test_Get_Success()
        {
            const string expectedUri = @"https://test.com/get";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");
            response.Data = testEntity;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.GetById<TestEntity>(expectedUri);

            Assert.That(result, Is.EqualTo(testEntity));
        }

        [Test]
        public void Test_GetList_Fail()
        {
            const string expectedUri = @"https://test.com/get";

            var response = new ApiResponse<PagedResult<TestEntity>>(HttpStatusCode.NotFound, "Not Found");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<TestEntity>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.GetList<TestEntity>(expectedUri);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Test_GetList_Success()
        {
            const string expectedUri = @"https://test.com/get";

            var testEntities = new PagedResult<TestEntity>();
            testEntities.collection.Add(new TestEntity());

            var response = new ApiResponse<PagedResult<TestEntity>>(HttpStatusCode.OK, "OK");
            response.Data = testEntities;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequest<PagedResult<TestEntity>>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.GetList<TestEntity>(expectedUri).ToList();

            Assert.That(result[0], Is.EqualTo(testEntities.collection[0]));
        }

        [Test]
        public void Test_Update_Entity_Fail()
        {
            const string expectedUri = @"https://test.com/update";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, "Not Found");
            response.Data = testEntity;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), testEntity)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update<TestEntity>(expectedUri, testEntity);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Update_Entity_Success()
        {
            const string expectedUri = @"https://test.com/update";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");
            response.Data = testEntity;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), testEntity)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update<TestEntity>(expectedUri, testEntity);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Update_Entity_Success_No_Data()
        {
            const string expectedUri = @"https://test.com/update";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), testEntity)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update<TestEntity>(expectedUri, testEntity);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Update_Fail()
        {
            const string expectedUri = @"https://test.com/update";

            var status = new StatusResponse();
            status.Error = "FooError";
            status.Errors.Add(new ErrorMessage {error_message = "BarError"});

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, "Not Found");
            response.Data = status;

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update(expectedUri);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("FooError\r\nBarError"));
        }

        [Test]
        public void Test_Update_Params_Fail()
        {
            const string expectedUri = @"https://test.com/update";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, "Not Found");
            response.Data = testEntity;

            var parameters = new Dictionary<string, object>();
            parameters.Add("foo", "bar");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), parameters)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update<TestEntity>(expectedUri, parameters);

            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Not Found"));
        }

        [Test]
        public void Test_Update_Params_Success()
        {
            const string expectedUri = @"https://test.com/update";

            var testEntity = new TestEntity();

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");
            response.Data = testEntity;

            var parameters = new Dictionary<string, object>();
            parameters.Add("foo", "bar");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), parameters)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update<TestEntity>(expectedUri, parameters);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Update_Params_Success_No_Data()
        {
            const string expectedUri = @"https://test.com/update";

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, "OK");

            var parameters = new Dictionary<string, object>();
            parameters.Add("foo", "bar");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<TestEntity>(It.Is<Uri>(y => y.ToString() == expectedUri), parameters)).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update<TestEntity>(expectedUri, parameters);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Test_Update_Success()
        {
            const string expectedUri = @"https://test.com/update";

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK, "OK");

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequest<StatusResponse>(It.Is<Uri>(y => y.ToString() == expectedUri))).Returns(response);

            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = endpoint.Update(expectedUri);

            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));
        }

        private class TestEndpoint : Endpoint
        {
            public TestEndpoint(ISoundCloudApiGateway gateway)
                : base(gateway)
            {
            }

            public IWebResult<T> Create<T>(string uri, Entity entity) where T : Entity
            {
                return Create<T>(new Uri(uri), entity);
            }

            public IWebResult<T> Create<T>(string uri, Dictionary<string, object> parameters) where T : Entity
            {
                return Create<T>(new Uri(uri), parameters);
            }

            public IWebResult Delete(string uri)
            {
                return Delete(new Uri(uri));
            }

            public T GetById<T>(string uri) where T : Entity
            {
                return GetById<T>(new Uri(uri));
            }

            public IEnumerable<T> GetList<T>(string uri) where T : Entity
            {
                return GetList<T>(new Uri(uri));
            }

            public IWebResult Update(string uri)
            {
                return Update(new Uri(uri));
            }

            public IWebResult<T> Update<T>(string uri, Entity entity) where T : Entity
            {
                return Update<T>(new Uri(uri), entity);
            }

            public IWebResult<T> Update<T>(string uri, Dictionary<string, object> parameters) where T : Entity
            {
                return Update<T>(new Uri(uri), parameters);
            }
        }

        private class TestEntity : Entity
        {
            internal override void AppendCredentialsToProperties(SoundCloudCredentials credentials)
            {
            }
        }
    }
}