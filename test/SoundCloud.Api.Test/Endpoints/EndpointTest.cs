using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EndpointTest
    {
        [Test]
        public async Task Create_Entity_Fail()
        {
            var expectedUri = new Uri("https://test.com/create");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, testEntity);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<TestEntity>(expectedUri, testEntity)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.CreateAsync<TestEntity>(expectedUri, testEntity);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Create_Entity_Success()
        {
            var expectedUri = new Uri("https://test.com/create");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, testEntity);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<TestEntity>(expectedUri, testEntity)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.CreateAsync<TestEntity>(expectedUri, testEntity);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Create_Entity_Success_No_Data()
        {
            var expectedUri = new Uri("https://test.com/create");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<TestEntity>(expectedUri, testEntity)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.CreateAsync<TestEntity>(expectedUri, testEntity);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Create_Params_Fail()
        {
            var expectedUri = new Uri("https://test.com/create");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, testEntity);

            var parameters = new Dictionary<string, object> { { "foo", "bar" } };

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<TestEntity>(expectedUri, parameters)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.CreateAsync<TestEntity>(expectedUri, parameters);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Create_Params_Success()
        {
            var expectedUri = new Uri("https://test.com/create");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, testEntity);

            var parameters = new Dictionary<string, object> { { "foo", "bar" } };

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<TestEntity>(expectedUri, parameters)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.CreateAsync<TestEntity>(expectedUri, parameters);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Create_Params_Success_No_Data()
        {
            var expectedUri = new Uri("https://test.com/create");

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK);

            var parameters = new Dictionary<string, object> { { "foo", "bar" } };

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeCreateRequestAsync<TestEntity>(expectedUri, parameters)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.CreateAsync<TestEntity>(expectedUri, parameters);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Delete_Fail()
        {
            var expectedUri = new Uri("https://test.com/delete");

            var status = new StatusResponse { Error = "Foo", Errors = new List<ErrorMessage> { new ErrorMessage { Message = "Bar" } } };
            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, status);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.DeleteAsync(expectedUri);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Foo\r\nBar"));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Delete_Success()
        {
            var expectedUri = new Uri("https://test.com/delete");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeDeleteRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.DeleteAsync(expectedUri);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Get_Fail()
        {
            var expectedUri = new Uri("https://test.com/get");

            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<TestEntity>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.GetByIdAsync<TestEntity>(expectedUri);

            // Assert
            Assert.That(result, Is.Null);

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Get_Success()
        {
            var expectedUri = new Uri("https://test.com/get");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, testEntity);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<TestEntity>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.GetByIdAsync<TestEntity>(expectedUri);

            // Assert
            Assert.That(result, Is.EqualTo(testEntity));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task GetList_Fail()
        {
            var expectedUri = new Uri("https://test.com/get");

            var response = new ApiResponse<PagedResult<TestEntity>>(HttpStatusCode.NotFound);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<TestEntity>>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.GetListAsync<TestEntity>(expectedUri);

            // Assert
            Assert.That(result, Is.Empty);

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task GetList_Success()
        {
            var expectedUri = new Uri("https://test.com/get");

            var testEntities = new PagedResult<TestEntity> { Collection = new List<TestEntity> { new TestEntity() } };
            var response = new ApiResponse<PagedResult<TestEntity>>(HttpStatusCode.OK, testEntities);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeGetRequestAsync<PagedResult<TestEntity>>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = (await endpoint.GetListAsync<TestEntity>(expectedUri)).ToList();

            // Assert
            Assert.That(result[0], Is.EqualTo(testEntities.Collection[0]));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Entity_Fail()
        {
            var expectedUri = new Uri("https://test.com/update");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, testEntity);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<TestEntity>(expectedUri, testEntity)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync<TestEntity>(expectedUri, testEntity);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Entity_Success()
        {
            var expectedUri = new Uri("https://test.com/update");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, testEntity);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<TestEntity>(expectedUri, testEntity)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync<TestEntity>(expectedUri, testEntity);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Entity_Success_No_Data()
        {
            var expectedUri = new Uri("https://test.com/update");

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK);

            var testEntity = new TestEntity();
            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<TestEntity>(expectedUri, testEntity)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync<TestEntity>(expectedUri, testEntity);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Fail()
        {
            var expectedUri = new Uri("https://test.com/update");

            var status = new StatusResponse { Error = "Foo", Errors = new List<ErrorMessage> { new ErrorMessage { Message = "Bar" } } };

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.NotFound, status);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync(expectedUri);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<object>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Foo\r\nBar"));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Params_Fail()
        {
            var expectedUri = new Uri("https://test.com/update");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.NotFound, testEntity);

            var parameters = new Dictionary<string, object> { { "foo", "bar" } };

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<TestEntity>(expectedUri, parameters)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync<TestEntity>(expectedUri, parameters);

            // Assert
            Assert.That(result, Is.InstanceOf<ErrorWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(HttpStatusCode.NotFound.ToString()));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Params_Success()
        {
            var expectedUri = new Uri("https://test.com/update");

            var testEntity = new TestEntity();
            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK, testEntity);

            var parameters = new Dictionary<string, object> { { "foo", "bar" } };

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<TestEntity>(expectedUri, parameters)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync<TestEntity>(expectedUri, parameters);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(testEntity));
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Params_Success_No_Data()
        {
            var expectedUri = new Uri("https://test.com/update");

            var response = new ApiResponse<TestEntity>(HttpStatusCode.OK);

            var parameters = new Dictionary<string, object> { { "foo", "bar" } };

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<TestEntity>(expectedUri, parameters)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync<TestEntity>(expectedUri, parameters);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<TestEntity>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        [Test]
        public async Task Update_Success()
        {
            var expectedUri = new Uri("https://test.com/update");

            var response = new ApiResponse<StatusResponse>(HttpStatusCode.OK);

            var gatewayMock = new Mock<ISoundCloudApiGateway>(MockBehavior.Strict);
            gatewayMock.Setup(x => x.InvokeUpdateRequestAsync<StatusResponse>(expectedUri)).ReturnsAsync(response);

            // Act
            var endpoint = new TestEndpoint(gatewayMock.Object);
            var result = await endpoint.UpdateAsync(expectedUri);

            // Assert
            Assert.That(result, Is.InstanceOf<SuccessWebResult<object>>());
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.ErrorMessage, Is.EqualTo(string.Empty));

            gatewayMock.VerifyAll();
        }

        private class TestEndpoint : Endpoint
        {
            public TestEndpoint(ISoundCloudApiGateway gateway)
                : base(gateway)
            {
            }

            public new Task<IWebResult<T>> CreateAsync<T>(Uri uri, Entity entity) where T : Entity
            {
                return base.CreateAsync<T>(uri, entity);
            }

            public Task<IWebResult<T>> CreateAsync<T>(Uri uri, Dictionary<string, object> parameters) where T : Entity
            {
                return base.CreateAsync<T>(uri, parameters);
            }

            public new Task<IWebResult> DeleteAsync(Uri uri)
            {
                return base.DeleteAsync(uri);
            }

            public new Task<T> GetByIdAsync<T>(Uri uri) where T : Entity
            {
                return base.GetByIdAsync<T>(uri);
            }

            public new Task<IEnumerable<T>> GetListAsync<T>(Uri uri) where T : Entity
            {
                return base.GetListAsync<T>(uri);
            }

            public new Task<IWebResult> UpdateAsync(Uri uri)
            {
                return base.UpdateAsync(uri);
            }

            public new Task<IWebResult<T>> UpdateAsync<T>(Uri uri, Entity entity) where T : Entity
            {
                return base.UpdateAsync<T>(uri, entity);
            }

            public Task<IWebResult<T>> UpdateAsync<T>(Uri uri, Dictionary<string, object> parameters) where T : Entity
            {
                return base.UpdateAsync<T>(uri, parameters);
            }
        }

        private class TestEntity : Entity
        {
        }
    }
}