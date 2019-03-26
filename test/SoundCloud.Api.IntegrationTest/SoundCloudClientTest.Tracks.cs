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

            Assert.That(postResult.Data.Title, Is.EqualTo(title));

            var postedTrack = postResult.Data;
            postedTrack.Commentable = false;
            postedTrack.Description = "TestDescription";
            postedTrack.DownloadUrl = new Uri("http://sampleurl.com");
            postedTrack.Downloadable = true;
            postedTrack.Genre = "SampleGenre";
            postedTrack.LabelName = "MySampleLabel";
            postedTrack.License = License.CcBy;
            postedTrack.PurchaseUrl = new Uri("http://sampleurl.com");
            postedTrack.ReleaseDay = 10;
            postedTrack.ReleaseMonth = 10;
            postedTrack.ReleaseYear = 2010;
            postedTrack.Sharing = Sharing.Public;
            postedTrack.TagList = new List<string> { "Tag1", "Tag2" };
            postedTrack.Title = "NewTitle";
            postedTrack.TrackType = TrackType.Sample;

            var updateResult = await client.Tracks.UploadArtworkAsync(postedTrack, TestDataProvider.GetArtwork());

            Assert.That(updateResult.Data.ArtworkUrl, Is.Not.Null);

            updateResult = await client.Tracks.UpdateAsync(postedTrack);

            Assert.That(updateResult.Data.Description, Is.EqualTo(postedTrack.Description));
            Assert.That(updateResult.Data.DownloadUrl.ToString(), Does.Contain("https://api.soundcloud.com/tracks/" + postedTrack.Id + "/download"));
            Assert.That(updateResult.Data.Downloadable, Is.EqualTo(postedTrack.Downloadable));
            Assert.That(updateResult.Data.Genre, Is.EqualTo(postedTrack.Genre));
            Assert.That(updateResult.Data.LabelName, Is.EqualTo(postedTrack.LabelName));
            Assert.That(updateResult.Data.License, Is.EqualTo(postedTrack.License));
            Assert.That(updateResult.Data.PurchaseUrl, Is.EqualTo(postedTrack.PurchaseUrl));
            Assert.That(updateResult.Data.ReleaseDay, Is.EqualTo(postedTrack.ReleaseDay));
            Assert.That(updateResult.Data.ReleaseMonth, Is.EqualTo(postedTrack.ReleaseMonth));
            Assert.That(updateResult.Data.ReleaseYear, Is.EqualTo(postedTrack.ReleaseYear));
            Assert.That(updateResult.Data.Sharing, Is.EqualTo(postedTrack.Sharing));
            Assert.That(updateResult.Data.TagList.Contains("Tag1"), Is.True);
            Assert.That(updateResult.Data.TagList.Contains("Tag2"), Is.True);
            Assert.That(updateResult.Data.Title, Is.EqualTo(postedTrack.Title));
            Assert.That(updateResult.Data.TrackType, Is.EqualTo(postedTrack.TrackType));

            await client.Tracks.DeleteAsync(postedTrack);

            Assert.Pass();
        }
    }
}