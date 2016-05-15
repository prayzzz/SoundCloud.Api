using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;
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
        public void Test_Playlists_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlist = new Playlist();
            playlist.title = "TestPlaylist";
            playlist.tracks = new List<Track>();
            playlist.tracks.Add(new Track {id = TrackId});
            playlist.tag_list = new List<string> {"Sampletag", "Sampletag2"};
            playlist.genre = "Sample";
            playlist.playlist_type = PlaylistType.Compilation;

            var postResult = client.Playlists.Post(playlist);
            Assert.IsTrue(postResult.IsSuccess);

            var requestPlaylist = client.Playlists.Get(postResult.Data.id);
            client.Playlists.Delete(postResult.Data);

            Assert.That(requestPlaylist, Is.Not.Null);
            Assert.That(requestPlaylist.tracks.Count, Is.GreaterThanOrEqualTo(1));
            Assert.That(requestPlaylist.uri.Query, Does.Contain("oauth_token=" + _settings.Token));
            Assert.That(requestPlaylist.created_with.uri.Query, Does.Contain("oauth_token=" + _settings.Token));
            Assert.That(requestPlaylist.tracks[0].uri.Query, Does.Contain("oauth_token=" + _settings.Token));
            Assert.That(requestPlaylist.user.uri.Query, Does.Contain("oauth_token=" + _settings.Token));
        }

        [Test]
        public void Test_Playlists_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var builder = new PlaylistQueryBuilder("diplo");
            builder.Representation = RepresentationMode.Compact;

            var playlist = client.Playlists.Get(builder).Take(10).ToList();

            Assert.That(playlist.Count, Is.EqualTo(10));
        }

        [Test]
        public void Test_Playlists_GetSecretToken()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlist = new Playlist();
            playlist.title = "TestPlaylist";
            playlist.tracks = new List<Track>();
            playlist.tracks.Add(new Track {id = TrackId});
            playlist.tag_list = new List<string> {"Sampletag", "Sampletag2"};
            playlist.genre = "Sample";
            playlist.playlist_type = PlaylistType.Compilation;

            var postResult = client.Playlists.Post(playlist);
            Assert.IsTrue(postResult.IsSuccess);

            var token = client.Playlists.GetSecretToken(postResult.Data);
            client.Playlists.Delete(postResult.Data);

            Assert.That(string.IsNullOrWhiteSpace(token.token), Is.False);
        }

        [Test]
        public void Test_Playlists_Post_Update_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlist = new Playlist();
            playlist.title = "TestPlaylist";
            playlist.tracks = new List<Track>();
            playlist.tracks.Add(new Track {id = TrackId});
            playlist.tag_list = new List<string> {"Sampletag", "Sampletag2"};
            playlist.genre = "Sample";
            playlist.playlist_type = PlaylistType.Compilation;

            var postResult = client.Playlists.Post(playlist);

            Assert.That(postResult.IsSuccess, Is.True);
            Assert.That(postResult.Data.tracks.Count, Is.EqualTo(1));
            Assert.That(postResult.Data.tracks.Any(x => x.id == TrackId), Is.True);
            Assert.That(postResult.Data.tag_list.Count, Is.EqualTo(2));
            Assert.That(postResult.Data.tag_list.Contains("Sampletag"), Is.True);
            Assert.That(postResult.Data.tag_list.Contains("Sampletag2"), Is.True);
            Assert.That(postResult.Data.genre, Is.EqualTo(playlist.genre));
            Assert.That(postResult.Data.title, Is.EqualTo(playlist.title));

            var postedPlaylist = postResult.Data;
            postedPlaylist.title = "New Title";
            postedPlaylist.tag_list = new List<string> {"Sampletag3"};
            postedPlaylist.genre = "Sample2";

            var updatedPlaylist = client.Playlists.Update(postedPlaylist);

            Assert.That(updatedPlaylist.Data.tracks.Count, Is.EqualTo(1));
            Assert.That(updatedPlaylist.Data.tracks.Any(x => x.id == TrackId), Is.True);
            Assert.That(updatedPlaylist.Data.tag_list.Count, Is.EqualTo(1));
            Assert.That(updatedPlaylist.Data.tag_list.Contains("Sampletag3"), Is.True);
            Assert.That(updatedPlaylist.Data.genre, Is.EqualTo(postResult.Data.genre));

            updatedPlaylist = client.Playlists.UploadArtwork(updatedPlaylist.Data, TestDataProvider.GetArtwork());

            Assert.That(string.IsNullOrEmpty(updatedPlaylist.Data.artwork_url), Is.False);

            client.Playlists.Delete(postResult.Data);

            Assert.Pass();
        }
    }
}