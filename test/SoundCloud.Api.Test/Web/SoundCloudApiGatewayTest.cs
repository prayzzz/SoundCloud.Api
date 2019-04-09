using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Test.Web
{
    [TestFixture]
    public class SoundCloudApiGatewayTest
    {
        [Test]
        public async Task SendPostRequestAsyncWithEntity()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var entity = new Comment();
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendPostRequestAsync<Comment>(uri, entity);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Post));
        }

        [Test]
        public async Task SendPostRequestAsyncWithFormData()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var parameters = new Dictionary<string, object> { { "String", "My Data" }, { "Integer", 133742 } };
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendPostRequestAsync<Comment>(uri, parameters);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Post));

            var content = await captor.Value.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("String"));
            Assert.That(content, Does.Contain("My Data"));
            Assert.That(content, Does.Contain("Integer"));
            Assert.That(content, Does.Contain("133742"));
        }

        [Test]
        public async Task SendPutRequestAsyncWithEntity()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var entity = new Comment();
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendPutRequestAsync<Comment>(uri, entity);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Put));
        }

        [Test]
        public async Task SendPutRequestAsyncWithFormData()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var parameters = new Dictionary<string, object> { { "String", "My Data" }, { "Integer", 133742 } };
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendPutRequestAsync<Comment>(uri, parameters);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Put));

            var content = await captor.Value.Content.ReadAsStringAsync();
            Assert.That(content, Does.Contain("String"));
            Assert.That(content, Does.Contain("My Data"));
            Assert.That(content, Does.Contain("Integer"));
            Assert.That(content, Does.Contain("133742"));
        }

        [Test]
        public async Task SendPutRequestAsync()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendPutRequestAsync<Comment>(uri);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Put));
        }

        [Test]
        public async Task SendGetRequestAsync()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendGetRequestAsync<Comment>(uri);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Get));
        }

        [Test]
        public async Task SendDeleteRequestAsync()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new Comment { Body = "My Comment" }))
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var result = await new SoundCloudApiGateway(httpClientFactory.Object).SendDeleteRequestAsync<Comment>(uri);

            // Assert
            Assert.That(result.Body, Is.EqualTo("My Comment"));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Delete));
        }

        [Test]
        public void SendGetRequestAsync_Error()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("Error")
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(response);

            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(SoundCloudClient.HttpClientName)).Returns(new HttpClient(handler.Object));

            // Act
            var uri = new Uri("http://localhost:5000");
            var exception = Assert.ThrowsAsync<SoundCloudApiException>(async () =>
                await new SoundCloudApiGateway(httpClientFactory.Object).SendGetRequestAsync<Comment>(uri));

            // Assert
            Assert.That(exception.HttpStatusCode, Is.EqualTo(response.StatusCode));
            Assert.That(exception.HttpContent, Is.EqualTo(response.Content));

            var captor = new ArgumentCaptor<HttpRequestMessage>();
            handler.Protected().Verify("SendAsync", Times.Once(), captor.CaptureExpr(), ItExpr.IsAny<CancellationToken>());
            Assert.That(captor.Value.RequestUri, Is.EqualTo(uri));
            Assert.That(captor.Value.Method, Is.EqualTo(HttpMethod.Get));
        }
    }
}