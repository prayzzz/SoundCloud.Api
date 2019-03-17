//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Runtime.Serialization;
//
//using HttpMock;
//
//using NUnit.Framework;
//
//using SoundCloud.Api.Web;
//
//namespace SoundCloud.Api.Test.Web
//{
//    [TestFixture]
//    public class MultipartDataFormRequestBuilderTest
//    {
//        [Test]
//        public void Test_Add_Bool()
//        {
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("foo", true);
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(parameters);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post("/create")).GetBody();
//            Assert.That(body, Does.Not.Contain("foo"));
//            Assert.That(body, Does.Not.Contain("true"));
//        }
//
//        [Test]
//        public void Test_Add_Enum()
//        {
//            var key = "foo";
//            var value = TestEnum.Foo;
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(key, value);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post("/create")).GetBody();
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo\"\r\n\r\nfoo\r\n"));
//        }
//
//        [Test]
//        public void Test_Add_Int()
//        {
//            var key = "foo";
//            var value = 42;
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(key, value);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post("/create")).GetBody();
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo\"\r\n\r\n42\r\n"));
//        }
//
//        [Test]
//        public void Test_Add_List()
//        {
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("foo1", "bar");
//            parameters.Add("foo2", 42);
//            parameters.Add("foo3", TestEnum.Foo);
//            parameters.Add("foo4", new MemoryStream(new byte[] {0x46, 0x6F, 0x6F, 0x34, 0x32}));
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(parameters);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post(@"/create")).GetBody();
//
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo1\"\r\n\r\nbar"));
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo2\"\r\n\r\n42"));
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo3\"\r\n\r\nfoo"));
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo4\"; filename=\"foo4\"\r\n\r\nFoo42"));
//        }
//
//        [Test]
//        public void Test_Add_Remove_String()
//        {
//            var key = "foo";
//            var value = "bar";
//
//            var key2 = "foo2";
//            var value2 = "bar2";
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(key, value);
//            builder.Add(key2, value2);
//            builder.Remove(key2);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post("/create")).GetBody();
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo\"\r\n\r\nbar\r\n"));
//            Assert.That(body, Does.Not.Contain("Content-Disposition: form-data; name=\"foo2\"\r\n\r\nbar2\r\n"));
//        }
//
//        [Test]
//        public void Test_Add_Stream()
//        {
//            var key = "foo";
//            var value = new MemoryStream(new byte[] {0x46, 0x6F, 0x6F, 0x34, 0x32});
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(key, value);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post("/create")).GetBody();
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo\"; filename=\"foo\"\r\n\r\nFoo42\r\n"));
//        }
//
//        [Test]
//        public void Test_Add_String()
//        {
//            var key = "foo";
//            var value = "bar";
//
//            var builder = new MultipartDataFormRequestBuilder();
//            builder.Add(key, value);
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Ok();
//
//            var request = WebRequest.Create("http://localhost:9191/create");
//            request.Method = "POST";
//            request.ContentType = "application/json";
//            builder.ApplyTo(request);
//
//            request.GetResponse().Dispose();
//
//            var body = httpMock.AssertWasCalled(x => x.Post("/create")).GetBody();
//            Assert.That(body, Does.Contain("Content-Disposition: form-data; name=\"foo\"\r\n\r\nbar\r\n"));
//        }
//
//        private enum TestEnum
//        {
//            [EnumMember(Value = "foo")]
//            Foo
//        }
//    }
//}