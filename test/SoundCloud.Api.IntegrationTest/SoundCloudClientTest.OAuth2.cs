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
            credentials.ClientId = Settings.ClientId;
            credentials.ClientSecret = Settings.ClientSecret;

            var client = SoundCloudClient.CreateUnauthorized("ClientId");
            var postedCredentials = await client.OAuth2.ClientCredentialsAsync(credentials);

            Assert.That(postedCredentials.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.AccessToken), Is.False);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.RefreshToken), Is.False);
            Assert.That(postedCredentials.Data.ExpiresIn, Is.Not.Null);
        }

        [Test]
        public async Task OAuth2_Login()
        {
            var credentials = new Credentials();
            credentials.ClientId = Settings.ClientId;
            credentials.ClientSecret = Settings.ClientSecret;
            credentials.Username = Settings.Username;
            credentials.Password = Settings.Password;

            var client = SoundCloudClient.CreateUnauthorized("ClientId");
            var postedCredentials = await client.OAuth2.LoginAsync(credentials);

            Assert.That(postedCredentials.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.AccessToken), Is.False);
            Assert.That(string.IsNullOrEmpty(postedCredentials.Data.RefreshToken), Is.False);
            Assert.That(postedCredentials.Data.ExpiresIn, Is.Not.Null);
        }

        [Test]
        public async Task OAuth2_RefreshToken()
        {
            var client = SoundCloudClient.CreateUnauthorized("ClientId");

            var loginCredentials = new Credentials();
            loginCredentials.ClientId = Settings.ClientId;
            loginCredentials.ClientSecret = Settings.ClientSecret;
            loginCredentials.Username = Settings.Username;
            loginCredentials.Password = Settings.Password;

            var loginResult = await client.OAuth2.LoginAsync(loginCredentials);

            Assert.That(loginResult.IsSuccess, Is.True);

            var credentials = new Credentials();
            credentials.ClientId = Settings.ClientId;
            credentials.ClientSecret = Settings.ClientSecret;
            credentials.RefreshToken = loginResult.Data.RefreshToken;

            var refreshResult = await client.OAuth2.RefreshTokenAsync(credentials);

            Assert.That(refreshResult.IsSuccess, Is.True);
            Assert.That(string.IsNullOrEmpty(refreshResult.Data.AccessToken), Is.False);
            Assert.That(string.IsNullOrEmpty(refreshResult.Data.RefreshToken), Is.False);
            Assert.That(refreshResult.Data.ExpiresIn, Is.Not.Null);
        }
    }
}