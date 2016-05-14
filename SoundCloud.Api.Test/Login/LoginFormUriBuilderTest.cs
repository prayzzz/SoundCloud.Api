using NUnit.Framework;

using SoundCloud.Api.Login;

namespace SoundCloud.Api.Test.Login
{
    [TestFixture]
    public class LoginFormUriBuilderTest
    {
        private const string ClientId = "ClientId";
        private const string RedirectUri = "RedirectUri";

        [Test]
        public void Test_Create_With_Custom_Values()
        {
            var builder = new LoginFormUriBuilder(ClientId, RedirectUri);
            builder.Display = Display.Popup;
            builder.Scope = Scope.NonExpiring;
            builder.ResponseType = ResponseType.Code;

            var uri = builder.Create();

            Assert.That(uri.ToString(), Is.EqualTo("https://soundcloud.com/connect?client_id=ClientId&response_type=code&display=popup&scope=non-expiring&redirect_uri=RedirectUri"));
        }

        [Test]
        public void Test_Create_With_Default_Values()
        {
            var builder = new LoginFormUriBuilder(ClientId, RedirectUri);
            var uri = builder.Create();

            Assert.That(uri.ToString(), Is.EqualTo("https://soundcloud.com/connect?client_id=ClientId&response_type=token&display=&scope=&redirect_uri=RedirectUri"));
        }

        [Test]
        public void Test_Get_ClientId()
        {
            var builder = new LoginFormUriBuilder(ClientId, RedirectUri);

            Assert.That(builder.ClientId, Is.EqualTo(ClientId));
        }

        [Test]
        public void Test_Get_RedirectUri()
        {
            var builder = new LoginFormUriBuilder(ClientId, RedirectUri);

            Assert.That(builder.RedirectUri, Is.EqualTo(RedirectUri));
        }
    }
}