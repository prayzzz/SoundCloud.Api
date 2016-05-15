using NUnit.Framework;

using SoundCloud.Api.Entities;

namespace SoundCloud.Api.IntegrationTest
{
    public partial class SoundCloudClientTest
    {
        [Test]
        public void Test_OAuth2_ClientCredentials()
        {
            var credentials = new Credentials();
            credentials.client_id = _settings.ClientId;
            credentials.client_secret = _settings.ClientSecret;

            var client = SoundCloudClient.Create();
            var postedCredentials = client.OAuth2.ClientCredentials(credentials);

            Assert.That(postedCredentials.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.refresh_token), Is.False);
            Assert.That(postedCredentials.Data.expires_in, Is.Not.Null);
        }

        [Test]
        public void Test_OAuth2_Login()
        {
            var credentials = new Credentials();
            credentials.client_id = _settings.ClientId;
            credentials.client_secret = _settings.ClientSecret;
            credentials.username = _settings.Username;
            credentials.password = _settings.Password;

            var client = SoundCloudClient.Create();
            var postedCredentials = client.OAuth2.Login(credentials);

            Assert.That(postedCredentials.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.refresh_token), Is.False);
            Assert.That(postedCredentials.Data.expires_in, Is.Not.Null);
        }

        [Test]
        public void Test_OAuth2_RefreshToken()
        {
            var client = SoundCloudClient.Create();

            var loginCredentials = new Credentials();
            loginCredentials.client_id = _settings.ClientId;
            loginCredentials.client_secret = _settings.ClientSecret;
            loginCredentials.username = _settings.Username;
            loginCredentials.password = _settings.Password;

            var loginResult = client.OAuth2.Login(loginCredentials);

            Assert.That(loginResult.IsSuccess, Is.True);

            var credentials = new Credentials();
            credentials.client_id = _settings.ClientId;
            credentials.client_secret = _settings.ClientSecret;
            credentials.refresh_token = loginResult.Data.refresh_token;

            var refreshResult = client.OAuth2.RefreshToken(credentials);

            Assert.That(refreshResult.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(refreshResult.Data.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(refreshResult.Data.refresh_token), Is.False);
            Assert.That(refreshResult.Data.expires_in, Is.Not.Null);
        }
    }
}