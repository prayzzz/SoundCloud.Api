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
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var comment = new Comment { Body = "TestComment at " + DateTime.Now.ToLocalTime(), TrackId = TrackId };

            var result = await client.Comments.PostAsync(comment);

            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.Body, Is.EqualTo(comment.Body));

            await client.Comments.DeleteAsync(result.Data);

            Assert.Pass();
        }
        
        [Test]
        public async Task Test_Comment_Get()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var result = await client.Comments.GetAsync(256985338);

            Assert.That(result.Body, Does.Contain("TestComment"));
        }
        
        [Test]
        public async Task Test_Comment_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var result = await client.Comments.GetAsync();

            var someComments = result.Take(100).ToList();
            Assert.That(someComments.Count, Is.EqualTo(100));
        }
    }
}