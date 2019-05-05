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
    public class PlaylistTest : IntegrationTestBase
    {
        [Test]
        public async Task Playlists_Get()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlist = new Playlist();
            playlist.Title = "TestPlaylist";
            playlist.Tracks = new List<Track> { new Track { Id = TrackId } };
            playlist.TagList = new List<string> { "Sampletag", "Sampletag2" };
            playlist.Genre = "Sample";
            playlist.PlaylistType = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.That(postResult.Title, Is.EqualTo(playlist.Title));

            var getResult = await client.Playlists.GetAsync(postResult.Id);
            Assert.That(getResult, Is.Not.Null);
            Assert.That(getResult.Tracks.Count, Is.GreaterThanOrEqualTo(1));

            var deleteResult = await client.Playlists.DeleteAsync(postResult);
            Assert.That(deleteResult.Error, Is.Null.Or.Empty);
            Assert.That(deleteResult.Errors, Is.Empty);
        }

        [Test]
        public async Task Playlists_GetList()
        {
            var client = SoundCloudClient.CreateUnauthorized(Settings.ClientId);

            var builder = new PlaylistQueryBuilder { SearchString = "diplo", Representation = RepresentationMode.Compact, Limit = 10 };

            var result = await client.Playlists.GetAllAsync(builder);
            Assert.That(result.Any(), Is.True);

            if (result.HasNextPage)
            {
                var nextResult = await result.GetNextPageAsync();
                Assert.That(nextResult.Any(), Is.True);

                Assert.That(result.First().Id, Is.Not.EqualTo(nextResult.First().Id));
            }
        }

        [Test]
        public async Task Playlists_GetSecretToken()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlist = new Playlist();
            playlist.Title = "TestPlaylist";
            playlist.Tracks = new List<Track> { new Track { Id = TrackId } };
            playlist.TagList = new List<string> { "Sampletag", "Sampletag2" };
            playlist.Genre = "Sample";
            playlist.PlaylistType = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.That(postResult.Title, Is.EqualTo(playlist.Title));

            var token = await client.Playlists.GetSecretTokenAsync(postResult);
            Assert.That(token.Token, Is.Not.Empty);

            var deleteResult = await client.Playlists.DeleteAsync(postResult);
            Assert.That(deleteResult.Error, Is.Null.Or.Empty);
            Assert.That(deleteResult.Errors, Is.Empty);
        }

        [Test]
        public async Task Playlists_Post_Update_Delete()
        {
            var client = SoundCloudClient.CreateAuthorized(Settings.Token);

            var playlist = new Playlist();
            playlist.Title = "TestPlaylist";
            playlist.Tracks = new List<Track> { new Track { Id = TrackId } };
            playlist.TagList = new List<string> { "Sampletag", "Sampletag2" };
            playlist.Genre = "Sample";
            playlist.PlaylistType = PlaylistType.Compilation;

            var postResult = await client.Playlists.PostAsync(playlist);
            Assert.That(postResult.Tracks, Has.Exactly(1).Items);
            Assert.That(postResult.Tracks, Has.One.Matches<Track>(x => x.Id == TrackId));
            Assert.That(postResult.TagList, Has.Exactly(2).Items);
            Assert.That(postResult.TagList, Has.Member("Sampletag"));
            Assert.That(postResult.TagList, Has.Member("Sampletag2"));
            Assert.That(postResult.Genre, Is.EqualTo(playlist.Genre));
            Assert.That(postResult.Title, Is.EqualTo(playlist.Title));

            postResult.Title = "New Title";
            postResult.TagList = new List<string> { "Sampletag3" };
            postResult.Genre = "Sample2";

            var updateResult = await client.Playlists.UpdateAsync(postResult);
            Assert.That(updateResult.Tracks, Has.Exactly(1).Items);
            Assert.That(updateResult.Tracks, Has.One.Matches<Track>(x => x.Id == TrackId));
            Assert.That(updateResult.TagList, Has.Exactly(1).Items);
            Assert.That(updateResult.TagList, Has.Member("Sampletag3"));
            Assert.That(updateResult.Genre, Is.EqualTo(postResult.Genre));

            var uploadArtworkResult = await client.Playlists.UploadArtworkAsync(updateResult, TestDataProvider.GetArtwork());
            Assert.That(uploadArtworkResult.ArtworkUrl, Is.Not.Empty);

            var deleteResult = await client.Playlists.DeleteAsync(postResult);
            Assert.That(deleteResult.Error, Is.Null.Or.Empty);
            Assert.That(deleteResult.Errors, Is.Empty);
        }
    }
}
