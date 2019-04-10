using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal class Playlists : IPlaylists
    {
        private const string PlaylistArtworkDataKey = "playlist[artwork_data]";
        private const string PlaylistPath = "playlists/{0}?";
        private const string PlaylistSecretTokenPath = "playlists/{0}/secret-token?";
        private const string PlaylistsPath = "playlists?";

        private readonly ISoundCloudApiGateway _gateway;

        public Playlists(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<StatusResponse> DeleteAsync(Playlist playlist)
        {
            playlist.ValidateDelete();

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await _gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<Playlist> GetAsync(int playlistId)
        {
            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlistId) };
            return await _gateway.SendGetRequestAsync<Playlist>(builder.BuildUri());
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync(string searchString, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0)
        {
            return await GetAllAsync(new PlaylistQueryBuilder { SearchString = searchString, Limit = limit, Offset = offset });
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync(PlaylistQueryBuilder queryBuilder)
        {
            queryBuilder.Path = PlaylistsPath;
            queryBuilder.Paged = true;

            return (await _gateway.SendGetRequestAsync<PagedResult<Playlist>>(queryBuilder.BuildUri())).Collection;
        }

        public async Task<SecretToken> GetSecretTokenAsync(Playlist playlist)
        {
            playlist.ValidateGet();

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistSecretTokenPath, playlist.Id) };
            return await _gateway.SendGetRequestAsync<SecretToken>(builder.BuildUri());
        }

        public async Task<Playlist> PostAsync(Playlist playlist)
        {
            playlist.ValidatePost();

            var builder = new PlaylistQueryBuilder { Path = PlaylistsPath };
            return await _gateway.SendPostRequestAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<Playlist> UpdateAsync(Playlist playlist)
        {
            playlist.ValidateGet();

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await _gateway.SendPutRequestAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<Playlist> UploadArtworkAsync(Playlist playlist, Stream file)
        {
            playlist.ValidateUploadArtwork();

            var parameters = new Dictionary<string, object> { { PlaylistArtworkDataKey, file } };
            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await _gateway.SendPutRequestAsync<Playlist>(builder.BuildUri(), parameters);
        }
    }
}