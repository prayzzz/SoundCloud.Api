using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Test.Data;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class TracksTest : SoundCloudClientTest
    {
        [Test]
        public async Task Tracks_Get()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var tracks = await client.Tracks.GetAsync(TrackId);

            Assert.That(tracks, Is.Not.Null);
            Assert.That(tracks.Genre, Is.EqualTo("Sample"));
            Assert.That(tracks.PlaybackCount, Is.GreaterThan(0));
        }

        [Test]
        public async Task Tracks_GetComments()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var track = new Track();
            track.Id = Track2Id;

            var comments = await client.Tracks.GetCommentsAsync(track);

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public async Task Tracks_GetFavoriters()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var track = new Track();
            track.Id = Track2Id;

            var users = await client.Tracks.GetFavoritersAsync(track);

            Assert.That(users.Any(), Is.True);
        }

        [Test]
        public async Task Tracks_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var tracks = (await client.Tracks.GetAsync()).Take(150).ToList();

            Assert.That(tracks.Count, Is.EqualTo(150));
        }

        [Test]
        public async Task Tracks_GetSecretToken()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var track = new Track { Id = TrackId };

            var secretToken = await client.Tracks.GetSecretTokenAsync(track);

            Assert.That(string.IsNullOrEmpty(secretToken.Token), Is.False);
        }

        [Test]
        public async Task Tracks_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var title = "SampleTitle at " + DateTime.Now.ToLocalTime();
            var postResult = await client.Tracks.UploadTrackAsync(title, TestDataProvider.GetSound());
            Assert.That(postResult.Title, Is.EqualTo(title));

            postResult.Commentable = false;
            postResult.Description = "TestDescription";
            postResult.DownloadUrl = new Uri("http://sampleurl.com");
            postResult.Downloadable = true;
            postResult.Genre = "SampleGenre";
            postResult.LabelName = "MySampleLabel";
            postResult.License = License.CcBy;
            postResult.PurchaseUrl = new Uri("http://sampleurl.com");
            postResult.ReleaseDay = 10;
            postResult.ReleaseMonth = 10;
            postResult.ReleaseYear = 2010;
            postResult.Sharing = Sharing.Public;
            postResult.TagList = new List<string> { "Tag1", "Tag2" };
            postResult.Title = "NewTitle";
            postResult.TrackType = TrackType.Sample;

            var uploadArtworkResult = await client.Tracks.UploadArtworkAsync(postResult, TestDataProvider.GetArtwork());
            Assert.That(uploadArtworkResult.ArtworkUrl, Is.Not.Null);

            var updateResult = await client.Tracks.UpdateAsync(postResult);
            Assert.That(updateResult.Description, Is.EqualTo(postResult.Description));
            Assert.That(updateResult.DownloadUrl.ToString(), Does.Contain("https://api.soundcloud.com/tracks/" + postResult.Id + "/download"));
            Assert.That(updateResult.Downloadable, Is.EqualTo(postResult.Downloadable));
            Assert.That(updateResult.Genre, Is.EqualTo(postResult.Genre));
            Assert.That(updateResult.LabelName, Is.EqualTo(postResult.LabelName));
            Assert.That(updateResult.License, Is.EqualTo(postResult.License));
            Assert.That(updateResult.PurchaseUrl, Is.EqualTo(postResult.PurchaseUrl));
            Assert.That(updateResult.ReleaseDay, Is.EqualTo(postResult.ReleaseDay));
            Assert.That(updateResult.ReleaseMonth, Is.EqualTo(postResult.ReleaseMonth));
            Assert.That(updateResult.ReleaseYear, Is.EqualTo(postResult.ReleaseYear));
            Assert.That(updateResult.Sharing, Is.EqualTo(postResult.Sharing));
            Assert.That(updateResult.TagList, Has.Member("Tag1"));
            Assert.That(updateResult.TagList, Has.Member("Tag2"));
            Assert.That(updateResult.Title, Is.EqualTo(postResult.Title));
            Assert.That(updateResult.TrackType, Is.EqualTo(postResult.TrackType));

            var deleteResult = await client.Tracks.DeleteAsync(updateResult);
            Assert.That(deleteResult.Error, Is.Null.Or.Empty);
            Assert.That(deleteResult.Errors, Is.Empty);
        }
    }
}