using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    [Ignore("App registration is closed. Client")]
    public class OAuth2Test : SoundCloudClientTest
    {
        [Test]
        public async Task OAuth2_ClientCredentials()
        {
            var credentials = new Credentials();
            credentials.client_id = _settings.ClientId;
            credentials.client_secret = _settings.ClientSecret;

            var client = SoundCloudClient.CreateUnauthorized("ClientId");
            var postedCredentials = await client.OAuth2.ClientCredentialsAsync(credentials);

            Assert.That(postedCredentials.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.refresh_token), Is.False);
            Assert.That(postedCredentials.Data.expires_in, Is.Not.Null);
        }

        [Test]
        public async Task OAuth2_Login()
        {
            var credentials = new Credentials();
            credentials.client_id = _settings.ClientId;
            credentials.client_secret = _settings.ClientSecret;
            credentials.username = _settings.Username;
            credentials.password = _settings.Password;

            var client = SoundCloudClient.CreateUnauthorized("ClientId");
            var postedCredentials = await client.OAuth2.LoginAsync(credentials);

            Assert.That(postedCredentials.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.refresh_token), Is.False);
            Assert.That(postedCredentials.Data.expires_in, Is.Not.Null);
        }

        [Test]
        public async Task OAuth2_RefreshToken()
        {
            var client = SoundCloudClient.CreateUnauthorized("ClientId");

            var loginCredentials = new Credentials();
            loginCredentials.client_id = _settings.ClientId;
            loginCredentials.client_secret = _settings.ClientSecret;
            loginCredentials.username = _settings.Username;
            loginCredentials.password = _settings.Password;

            var loginResult = await client.OAuth2.LoginAsync(loginCredentials);

            Assert.That(loginResult.IsSuccess, Is.True);

            var credentials = new Credentials();
            credentials.client_id = _settings.ClientId;
            credentials.client_secret = _settings.ClientSecret;
            credentials.refresh_token = loginResult.Data.refresh_token;

            var refreshResult = await client.OAuth2.RefreshTokenAsync(credentials);

            Assert.That(refreshResult.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(refreshResult.Data.access_token), Is.False);
            Assert.That(string.IsNullOrEmpty(refreshResult.Data.refresh_token), Is.False);
            Assert.That(refreshResult.Data.expires_in, Is.Not.Null);
        }
    }
}