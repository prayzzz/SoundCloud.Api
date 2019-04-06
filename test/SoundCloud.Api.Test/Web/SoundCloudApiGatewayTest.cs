//using System;
//using System.Collections.Generic;
//using System.Net;
//
//using HttpMock;
//
//using Newtonsoft.Json;
//
//using NUnit.Framework;
//
//using SoundCloud.Api.Entities.Base;
//using SoundCloud.Api.Utils;
//using SoundCloud.Api.Web;
//
//namespace SoundCloud.Api.Test.Web
//{
//    [TestFixture]
//    public class SoundCloudApiGatewayTest
//    {
//        [Test]
//        public void Test_Create_Entity_Fail()
//        {
//            var entity = new TestEntity {Value = "Bar"};
//
//            var uri = new Uri("http://localhost:9191/create");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeCreateRequestAsync<TestEntity>(uri, entity);
//
//            httpMock.AssertWasCalled(x => x.Post("/create"));
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Create_Entity_Success()
//        {
//            var entity = new TestEntity {Value = "Bar"};
//            var responseEntity = new TestEntity {Value = "Foo"};
//
//            var uri = new Uri("http://localhost:9191/create");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Return(JsonConvert.SerializeObject(responseEntity)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeCreateRequestAsync<TestEntity>(uri, entity);
//
//            httpMock.AssertWasCalled(x => x.Post("/create"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Value, Is.EqualTo(responseEntity.Value));
//        }
//
//        [Test]
//        public void Test_Create_Parameters_Fail()
//        {
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("foo", "bar");
//
//            var uri = new Uri("http://localhost:9191/create");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeCreateRequestAsync<TestEntity>(uri, parameters);
//
//            httpMock.AssertWasCalled(x => x.Post("/create"));
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Create_Parameters_Success()
//        {
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("foo", "bar");
//
//            var responseEntity = new TestEntity {Value = "Foo"};
//
//            var uri = new Uri("http://localhost:9191/create");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Post("/create")).Return(JsonConvert.SerializeObject(responseEntity)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeCreateRequestAsync<TestEntity>(uri, parameters);
//
//            httpMock.AssertWasCalled(x => x.Post("/create"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Value, Is.EqualTo(responseEntity.Value));
//        }
//
//        [Test]
//        public void Test_Delete_Fail()
//        {
//            var status = new Status {Message = "404 - Not Found"};
//            var uri = new Uri("http://localhost:9191/delete");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Delete("/delete")).Return(JsonConvert.SerializeObject(status)).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeDeleteRequestAsync<Status>(uri);
//
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Message, Is.EqualTo(status.Message));
//        }
//
//        [Test]
//        public void Test_Delete_Success()
//        {
//            var status = new Status {Message = "200 - OK"};
//            var uri = new Uri("http://localhost:9191/delete");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Delete("/delete")).Return(JsonConvert.SerializeObject(status)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeDeleteRequestAsync<Status>(uri);
//
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Message, Is.EqualTo(status.Message));
//        }
//
//        [Test]
//        public void Test_Get_Deserialization_Failed()
//        {
//            var uri = new Uri("http://localhost:9191/get");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Get("/get")).Return("{ { }").Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeGetRequestAsync<TestEntity>(uri);
//
//            httpMock.AssertWasCalled(x => x.Get("/get"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Get_Failed()
//        {
//            var uri = new Uri("http://localhost:9191/get");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Get("/get")).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeGetRequestAsync<TestEntity>(uri);
//
//            httpMock.AssertWasCalled(x => x.Get("/get"));
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Get_Success()
//        {
//            var entity = new TestEntity {Value = "Bar"};
//
//            var uri = new Uri("http://localhost:9191/get");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Get("/get")).Return(JsonConvert.SerializeObject(entity)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeGetRequestAsync<TestEntity>(uri);
//
//            httpMock.AssertWasCalled(x => x.Get("/get"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Value, Is.EqualTo(entity.Value));
//        }
//
//        [Test]
//        public void Test_Update_Entity_Fail()
//        {
//            var entity = new TestEntity {Value = "Bar"};
//
//            var uri = new Uri("http://localhost:9191/update");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Put("/update")).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeUpdateRequestAsync<TestEntity>(uri, entity);
//
//            httpMock.AssertWasCalled(x => x.Put("/update"));
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Update_Entity_Success()
//        {
//            var entity = new TestEntity {Value = "Bar"};
//            var responseEntity = new TestEntity {Value = "Foo"};
//
//            var uri = new Uri("http://localhost:9191/update");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Put("/update")).Return(JsonConvert.SerializeObject(responseEntity)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeUpdateRequestAsync<TestEntity>(uri, entity);
//
//            httpMock.AssertWasCalled(x => x.Put("/update"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Value, Is.EqualTo(responseEntity.Value));
//        }
//
//        [Test]
//        public void Test_Update_Fail()
//        {
//            var uri = new Uri("http://localhost:9191/update");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Put("/update")).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeUpdateRequestAsync<TestEntity>(uri);
//
//            httpMock.AssertWasCalled(x => x.Put("/update"));
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Update_Parameters_Fail()
//        {
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("foo", "bar");
//
//            var uri = new Uri("http://localhost:9191/update");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Put("/update")).NotFound();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeUpdateRequestAsync<TestEntity>(uri, parameters);
//
//            httpMock.AssertWasCalled(x => x.Put("/update"));
//            Assert.That(response.IsSuccess, Is.False);
//            Assert.That(response.IsError, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
//            Assert.That(response.ContainsData, Is.False);
//            Assert.That(response.Data, Is.Null);
//        }
//
//        [Test]
//        public void Test_Update_Parameters_Success()
//        {
//            var parameters = new Dictionary<string, object>();
//            parameters.Add("foo", "bar");
//
//            var responseEntity = new TestEntity {Value = "Foo"};
//
//            var uri = new Uri("http://localhost:9191/update");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Put("/update")).Return(JsonConvert.SerializeObject(responseEntity)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeUpdateRequestAsync<TestEntity>(uri, parameters);
//
//            httpMock.AssertWasCalled(x => x.Put("/update"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Value, Is.EqualTo(responseEntity.Value));
//        }
//
//        [Test]
//        public void Test_Update_Success()
//        {
//            var responseEntity = new TestEntity {Value = "Foo"};
//
//            var uri = new Uri("http://localhost:9191/update");
//
//            var httpMock = HttpMockRepository.At("http://localhost:9191");
//            httpMock.Stub(x => x.Put("/update")).Return(JsonConvert.SerializeObject(responseEntity)).Ok();
//
//            var gateway = new SoundCloudApiGateway();
//            var response = gateway.InvokeUpdateRequestAsync<TestEntity>(uri);
//
//            httpMock.AssertWasCalled(x => x.Put("/update"));
//            Assert.That(response.IsSuccess, Is.True);
//            Assert.That(response.IsError, Is.False);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.ContainsData, Is.True);
//            Assert.That(response.Data.Value, Is.EqualTo(responseEntity.Value));
//        }
//
//        private class Status
//        {
//            public string Message { get; set; }
//        }
//
//        private class TestEntity : Entity
//        {
//            public string Value { get; set; }
//
//            internal override void AppendCredentialsToProperties(SoundCloudCredentials credentials)
//            {
//            }
//
//            internal override BoxedEntity ToBoxedEntity()
//            {
//                return new TestEntityBox(this);
//            }
//
//            private class TestEntityBox : BoxedEntity
//            {
//                public TestEntityBox(TestEntity testEntity)
//                {
//                    TestEntity = testEntity;
//                }
//
//                public TestEntity TestEntity { get; set; }
//            }
//        }
//    }
//}

