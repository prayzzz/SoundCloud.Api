using System.Threading.Tasks;
using NUnit.Framework;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class SoundCloudOAuthTest : IntegrationTestBase
    {
        [Test]
        public async Task TestClientCredentials()
        {
            // Act
            var credentials = await SoundCloudOAuth.FromClientCredentials(Settings.ClientId, Settings.ClientSecret);
            
            // Assert
            Assert.That(credentials.AccessToken, Is.Not.Null);
        }
        
        [Test]
        public async Task TestPassword()
        {
            // Act
            var credentials = await SoundCloudOAuth.FromPassword(Settings.ClientId, Settings.ClientSecret, Settings.Username, Settings.Password);
            
            // Assert
            Assert.That(credentials.AccessToken, Is.Not.Null);
        }
        
        [Test]
        public async Task TestRefreshToken()
        {
            var initial = await SoundCloudOAuth.FromClientCredentials(Settings.ClientId, Settings.ClientSecret);
            
            // Act
            var credentials = await SoundCloudOAuth.FromRefreshToken(Settings.ClientId, Settings.ClientSecret, initial.RefreshToken);
            
            // Assert
            Assert.That(credentials.AccessToken, Is.Not.Null);
        }
    }
}
