using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class CommentsTest : SoundCloudClientTest
    {
        [Test]
        public async Task Test_Comment_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var comment = new Comment { body = "TestComment at " + DateTime.Now.ToLocalTime(), track_id = TrackId };

            var result = await client.Comments.PostAsync(comment);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.body, Is.EqualTo(comment.body));

            await client.Comments.DeleteAsync(result.Data);

            Assert.Pass();
        }
        
        [Test]
        public async Task Test_Comment_Get()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var result = await client.Comments.GetAsync(256985338);

            Assert.That(result.body, Does.Contain("TestComment"));
        }
        
        [Test]
        public async Task Test_Comment_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var result = await client.Comments.GetAsync();

            var someComments = result.Take(100).ToList();
            Assert.That(someComments.Count, Is.EqualTo(100));
        }
    }
}