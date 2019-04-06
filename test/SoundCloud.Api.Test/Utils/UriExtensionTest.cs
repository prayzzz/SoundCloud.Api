//using System;
//
//using NUnit.Framework;
//
//using SoundCloud.Api.Utils;
//
//namespace SoundCloud.Api.Test.Utils
//{
//    [TestFixture]
//    public class UriExtensionTest
//    {
//        [Test]
//        public void Test_AppendCredentials_AccessToken()
//        {
//            var credentials = new SoundCloudCredentials();
//            credentials.AccessToken = "token";
//
//            var uri = new Uri("http://test.com/?");
//            uri = uri.AppendCredentials(credentials);
//
//            Assert.That(uri.ToString(), Is.EqualTo("http://test.com/?oauth_token=token"));
//        }
//
//        [Test]
//        public void Test_AppendCredentials_Ampersand_Delimiter()
//        {
//            var credentials = new SoundCloudCredentials();
//            credentials.ClientId = "clientId";
//
//            var uri = new Uri("http://test.com/?query=value");
//            uri = uri.AppendCredentials(credentials);
//
//            Assert.That(uri.ToString(), Is.EqualTo("http://test.com/?query=value&client_id=clientId"));
//        }
//
//        [Test]
//        public void Test_AppendCredentials_ClientId()
//        {
//            var credentials = new SoundCloudCredentials();
//            credentials.ClientId = "clientId";
//
//            var uri = new Uri("http://test.com/?");
//            uri = uri.AppendCredentials(credentials);
//
//            Assert.That(uri.ToString(), Is.EqualTo("http://test.com/?client_id=clientId"));
//        }
//
//        [Test]
//        public void Test_AppendCredentials_No_Credentials()
//        {
//            var credentials = new SoundCloudCredentials();
//
//            var uri = new Uri("http://test.com/?");
//            uri = uri.AppendCredentials(credentials);
//
//            Assert.That(uri.ToString(), Is.EqualTo("http://test.com/?"));
//        }
//
//        [Test]
//        public void Test_AppendCredentials_No_Query()
//        {
//            var credentials = new SoundCloudCredentials();
//            credentials.ClientId = "clientId";
//
//            var uri = new Uri("http://test.com/");
//            uri = uri.AppendCredentials(credentials);
//
//            Assert.That(uri.ToString(), Is.EqualTo("http://test.com/?client_id=clientId"));
//        }
//    }
//}

