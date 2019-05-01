using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Playlists : Endpoint, IPlaylists
    {
        private const string PlaylistArtworkDataKey = "playlist[artwork_data]";
        private const string PlaylistPath = "playlists/{0}?";
        private const string PlaylistSecretTokenPath = "playlists/{0}/secret-token?";
        private const string PlaylistsPath = "playlists?";

        public Playlists(ISoundCloudApiGateway gateway) : base(gateway)
        {
        }

        public async Task<StatusResponse> DeleteAsync(Playlist playlist)
        {
            playlist.ValidateDelete();

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await Gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<Playlist> GetAsync(long playlistId)
        {
            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlistId) };
            return await Gateway.SendGetRequestAsync<Playlist>(builder.BuildUri());
        }

        public Task<SoundCloudList<Playlist>> GetAllAsync(string searchString, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            return GetAllAsync(new PlaylistQueryBuilder { SearchString = searchString, Limit = limit });
        }

        public Task<SoundCloudList<Playlist>> GetAllAsync(PlaylistQueryBuilder builder)
        {
            builder.Path = PlaylistsPath;
            builder.Paged = true;

            return GetPage<Playlist>(builder.BuildUri());
        }

        public async Task<SecretToken> GetSecretTokenAsync(Playlist playlist)
        {
            playlist.ValidateGet();

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistSecretTokenPath, playlist.Id) };
            return await Gateway.SendGetRequestAsync<SecretToken>(builder.BuildUri());
        }

        public async Task<Playlist> PostAsync(Playlist playlist)
        {
            playlist.ValidatePost();

            var builder = new PlaylistQueryBuilder { Path = PlaylistsPath };
            return await Gateway.SendPostRequestAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<Playlist> UpdateAsync(Playlist playlist)
        {
            playlist.ValidateGet();

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await Gateway.SendPutRequestAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<Playlist> UploadArtworkAsync(Playlist playlist, Stream file)
        {
            playlist.ValidateUploadArtwork();

            var parameters = new Dictionary<string, object> { { PlaylistArtworkDataKey, file } };
            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await Gateway.SendPutRequestAsync<Playlist>(builder.BuildUri(), parameters);
        }
    }
}
