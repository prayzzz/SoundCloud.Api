using System;

using NUnit.Framework;

using SoundCloud.Api.Entities;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public partial class SoundCloudClientTest
    {
        [Test]
        public void Test_Comment_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var comment = new Comment();
            comment.body = "TestComment at " + DateTime.Now.ToLocalTime();
            comment.track_id = TrackId;

            var result = client.Comments.Post(comment);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.body, Is.EqualTo(comment.body));
            Assert.That(result.Data.uri.Query, Does.Contain("oauth_token=" + _settings.Token));
            Assert.That(result.Data.user.uri.Query, Does.Contain("oauth_token=" + _settings.Token));

            client.Comments.Delete(result.Data);

            Assert.Pass();
        }
    }
}