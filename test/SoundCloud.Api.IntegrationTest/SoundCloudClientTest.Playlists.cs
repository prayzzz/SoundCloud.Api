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
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlist = new Playlist();
            playlist.Title = "TestPlaylist";
            playlist.Tracks = new List<Track>();
            playlist.Tracks.Add(new Track { Id = TrackId });
            playlist.TagList = new List<string> { "Sampletag", "Sampletag2" };
            playlist.Genre = "Sample";
            playlist.PlaylistType = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.IsTrue(postResult.IsSuccess);

            var requestPlaylist = await client.Playlists.GetAsync(postResult.Data.Id);
            await client.Playlists.DeleteAsync(postResult.Data);

            Assert.That(requestPlaylist, Is.Not.Null);
            Assert.That(requestPlaylist.Tracks.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public async Task Playlists_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var builder = new PlaylistQueryBuilder("diplo");
            builder.Representation = RepresentationMode.Compact;

            var playlist = (await client.Playlists.GetAsync(builder)).Take(10).ToList();

            Assert.That(playlist.Count, Is.EqualTo(10));
        }

        [Test]
        public async Task Playlists_GetSecretToken()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlist = new Playlist();
            playlist.Title = "TestPlaylist";
            playlist.Tracks = new List<Track>();
            playlist.Tracks.Add(new Track { Id = TrackId });
            playlist.TagList = new List<string> { "Sampletag", "Sampletag2" };
            playlist.Genre = "Sample";
            playlist.PlaylistType = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.IsTrue(postResult.IsSuccess);

            var token = await client.Playlists.GetSecretTokenAsync(postResult.Data);
            await client.Playlists.DeleteAsync(postResult.Data);

            Assert.That(string.IsNullOrWhiteSpace(token.Token), Is.False);
        }

        [Test]
        public async Task Playlists_Post_Update_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlist = new Playlist();
            playlist.Title = "TestPlaylist";
            playlist.Tracks = new List<Track>();
            playlist.Tracks.Add(new Track { Id = TrackId });
            playlist.TagList = new List<string> { "Sampletag", "Sampletag2" };
            playlist.Genre = "Sample";
            playlist.PlaylistType = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);

            Assert.That(postResult.IsSuccess, Is.True);
            Assert.That(postResult.Data.Tracks.Count, Is.EqualTo(1));
            Assert.That(postResult.Data.Tracks.Any(x => x.Id == TrackId), Is.True);
            Assert.That(postResult.Data.TagList.Count, Is.EqualTo(2));
            Assert.That(postResult.Data.TagList.Contains("Sampletag"), Is.True);
            Assert.That(postResult.Data.TagList.Contains("Sampletag2"), Is.True);
            Assert.That(postResult.Data.Genre, Is.EqualTo(playlist.Genre));
            Assert.That(postResult.Data.Title, Is.EqualTo(playlist.Title));

            var postedPlaylist = postResult.Data;
            postedPlaylist.Title = "New Title";
            postedPlaylist.TagList = new List<string> { "Sampletag3" };
            postedPlaylist.Genre = "Sample2";

            var updatedPlaylist = await client.Playlists.UpdateAsync(postedPlaylist);

            Assert.That(updatedPlaylist.Data.Tracks.Count, Is.EqualTo(1));
            Assert.That(updatedPlaylist.Data.Tracks.Any(x => x.Id == TrackId), Is.True);
            Assert.That(updatedPlaylist.Data.TagList.Count, Is.EqualTo(1));
            Assert.That(updatedPlaylist.Data.TagList.Contains("Sampletag3"), Is.True);
            Assert.That(updatedPlaylist.Data.Genre, Is.EqualTo(postResult.Data.Genre));

            updatedPlaylist = await  client.Playlists.UploadArtworkAsync(updatedPlaylist.Data, TestDataProvider.GetArtwork());

            Assert.That(string.IsNullOrEmpty(updatedPlaylist.Data.ArtworkUrl), Is.False);

            await client.Playlists.DeleteAsync(postResult.Data);

            Assert.Pass();
        }
    }
}