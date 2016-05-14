using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.Test.Data;

namespace SoundCloud.Api.IntegrationTest
{
    /// <summary>
    /// This class tests the logic of the wrapper against the real SoundCloud API.
    /// Therefore a clientid and token is needed. Both values are loaded from a settings.json file.
    /// In order to run this tests, the file must be provided.
    /// All tests are marked as inconclusive, if the file is not available.
    /// </summary>
    [TestFixture]
    public partial class SoundCloudClientTest
    {
        [Test]
        public void Test_Tracks_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var tracks = client.Tracks.Get(TrackId);

            Assert.That(tracks, Is.Not.Null);
            Assert.That(tracks.genre, Is.EqualTo("Sample"));
            Assert.That(tracks.playback_count, Is.GreaterThan(0));

            Assert.That(tracks.uri.Query, Does.Contain("oauth_token=" + _settings.Token));
            Assert.That(tracks.stream_url.Query, Does.Contain("oauth_token=" + _settings.Token));
            Assert.That(tracks.user.uri.Query, Does.Contain("oauth_token=" + _settings.Token));
        }

        [Test]
        public void Test_Tracks_GetComments()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var track = new Track();
            track.id = Track2Id;

            var comments = client.Tracks.GetComments(track);

            Assert.That(comments.Any(), Is.True);
        }

        [Test]
        public void Test_Tracks_GetFavoriters()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var track = new Track();
            track.id = Track2Id;

            var users = client.Tracks.GetFavoriters(track);

            Assert.That(users.Any(), Is.True);
        }

        [Test]
        public void Test_Tracks_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var tracks = client.Tracks.Get().Take(150).ToList();

            Assert.That(tracks.Count, Is.EqualTo(150));
        }

        [Test]
        public void Test_Tracks_GetSecretToken()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var track = new Track();
            track.id = TrackId;

            var secretToken = client.Tracks.GetSecretToken(track);

            Assert.That(string.IsNullOrEmpty(secretToken.token), Is.False);
        }

        [Test]
        public void Test_Tracks_Post_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var title = "SampleTitle at " + DateTime.Now.ToLocalTime();
            var postResult = client.Tracks.UploadTrack(title, TestDataProvider.GetSound());

            Assert.That(postResult.Data.title, Is.EqualTo(title));

            var postedTrack = postResult.Data;
            postedTrack.commentable = false;
            postedTrack.description = "TestDescription";
            postedTrack.download_url = new Uri("http://sampleurl.com");
            postedTrack.downloadable = true;
            postedTrack.genre = "SampleGenre";
            postedTrack.label_name = "MySampleLabel";
            postedTrack.license = License.CcBy;
            postedTrack.purchase_url = new Uri("http://sampleurl.com");
            postedTrack.release_day = 10;
            postedTrack.release_month = 10;
            postedTrack.release_year = 2010;
            postedTrack.sharing = Sharing.Public;
            postedTrack.tag_list = new List<string> {"Tag1", "Tag2"};
            postedTrack.title = "NewTitle";
            postedTrack.track_type = TrackType.Sample;

            var updateResult = client.Tracks.UploadArtwork(postedTrack, TestDataProvider.GetArtwork());

            Assert.That(updateResult.Data.artwork_url, Is.Not.Null);

            updateResult = client.Tracks.Update(postedTrack);

            Assert.That(updateResult.Data.description, Is.EqualTo(postedTrack.description));
            Assert.That(updateResult.Data.download_url, Does.Contain("https://api.soundcloud.com/tracks/" + postedTrack.id + "/download"));
            Assert.That(updateResult.Data.downloadable, Is.EqualTo(postedTrack.downloadable));
            Assert.That(updateResult.Data.genre, Is.EqualTo(postedTrack.genre));
            Assert.That(updateResult.Data.label_name, Is.EqualTo(postedTrack.label_name));
            Assert.That(updateResult.Data.license, Is.EqualTo(postedTrack.license));
            Assert.That(updateResult.Data.purchase_url, Is.EqualTo(postedTrack.purchase_url));
            Assert.That(updateResult.Data.release_day, Is.EqualTo(postedTrack.release_day));
            Assert.That(updateResult.Data.release_month, Is.EqualTo(postedTrack.release_month));
            Assert.That(updateResult.Data.release_year, Is.EqualTo(postedTrack.release_year));
            Assert.That(updateResult.Data.sharing, Is.EqualTo(postedTrack.sharing));
            Assert.That(updateResult.Data.tag_list.Contains("Tag1"), Is.True);
            Assert.That(updateResult.Data.tag_list.Contains("Tag2"), Is.True);
            Assert.That(updateResult.Data.title, Is.EqualTo(postedTrack.title));
            Assert.That(updateResult.Data.track_type, Is.EqualTo(postedTrack.track_type));

            client.Tracks.Delete(postedTrack);

            Assert.Pass();
        }
    }
}