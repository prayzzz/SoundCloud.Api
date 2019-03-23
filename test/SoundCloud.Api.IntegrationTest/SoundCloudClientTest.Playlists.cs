using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Entities.Enums;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Test.Data;

namespace SoundCloud.Api.IntegrationTest
{
    [TestFixture]
    public class PlaylistTest : SoundCloudClientTest
    {
        [Test]
        public async Task Playlists_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlist = new Playlist();
            playlist.title = "TestPlaylist";
            playlist.tracks = new List<Track>();
            playlist.tracks.Add(new Track { Id = TrackId });
            playlist.tag_list = new List<string> { "Sampletag", "Sampletag2" };
            playlist.genre = "Sample";
            playlist.playlist_type = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.IsTrue(postResult.IsSuccess);

            var requestPlaylist = await client.Playlists.GetAsync(postResult.Data.Id);
            await client.Playlists.DeleteAsync(postResult.Data);

            Assert.That(requestPlaylist, Is.Not.Null);
            Assert.That(requestPlaylist.tracks.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public async Task Playlists_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(_settings.ClientId);

            var builder = new PlaylistQueryBuilder("diplo");
            builder.Representation = RepresentationMode.Compact;

            var playlist = (await client.Playlists.GetAsync(builder)).Take(10).ToList();

            Assert.That(playlist.Count, Is.EqualTo(10));
        }

        [Test]
        public async Task Playlists_GetSecretToken()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlist = new Playlist();
            playlist.title = "TestPlaylist";
            playlist.tracks = new List<Track>();
            playlist.tracks.Add(new Track { Id = TrackId });
            playlist.tag_list = new List<string> { "Sampletag", "Sampletag2" };
            playlist.genre = "Sample";
            playlist.playlist_type = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.IsTrue(postResult.IsSuccess);

            var token = await client.Playlists.GetSecretTokenAsync(postResult.Data);
            await client.Playlists.DeleteAsync(postResult.Data);

            Assert.That(string.IsNullOrWhiteSpace(token.token), Is.False);
        }

        [Test]
        public async Task Playlists_Post_Update_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(_settings.Token);

            var playlist = new Playlist();
            playlist.title = "TestPlaylist";
            playlist.tracks = new List<Track>();
            playlist.tracks.Add(new Track { Id = TrackId });
            playlist.tag_list = new List<string> { "Sampletag", "Sampletag2" };
            playlist.genre = "Sample";
            playlist.playlist_type = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);

            Assert.That(postResult.IsSuccess, Is.True);
            Assert.That(postResult.Data.tracks.Count, Is.EqualTo(1));
            Assert.That(postResult.Data.tracks.Any(x => x.Id == TrackId), Is.True);
            Assert.That(postResult.Data.tag_list.Count, Is.EqualTo(2));
            Assert.That(postResult.Data.tag_list.Contains("Sampletag"), Is.True);
            Assert.That(postResult.Data.tag_list.Contains("Sampletag2"), Is.True);
            Assert.That(postResult.Data.genre, Is.EqualTo(playlist.genre));
            Assert.That(postResult.Data.title, Is.EqualTo(playlist.title));

            var postedPlaylist = postResult.Data;
            postedPlaylist.title = "New Title";
            postedPlaylist.tag_list = new List<string> { "Sampletag3" };
            postedPlaylist.genre = "Sample2";

            var updatedPlaylist = await client.Playlists.UpdateAsync(postedPlaylist);

            Assert.That(updatedPlaylist.Data.tracks.Count, Is.EqualTo(1));
            Assert.That(updatedPlaylist.Data.tracks.Any(x => x.Id == TrackId), Is.True);
            Assert.That(updatedPlaylist.Data.tag_list.Count, Is.EqualTo(1));
            Assert.That(updatedPlaylist.Data.tag_list.Contains("Sampletag3"), Is.True);
            Assert.That(updatedPlaylist.Data.genre, Is.EqualTo(postResult.Data.genre));

            updatedPlaylist = await  client.Playlists.UploadArtworkAsync(updatedPlaylist.Data, TestDataProvider.GetArtwork());

            Assert.That(string.IsNullOrEmpty(updatedPlaylist.Data.artwork_url), Is.False);

            await client.Playlists.DeleteAsync(postResult.Data);

            Assert.Pass();
        }
    }
}