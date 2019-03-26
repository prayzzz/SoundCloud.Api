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

        public Playlists(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<IWebResult> DeleteAsync(Playlist playlist)
        {
            Validate(playlist.ValidateDelete);

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<Playlist> GetAsync(int playlistId)
        {
            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlistId) };
            return await GetByIdAsync<Playlist>(builder.BuildUri());
        }

        public async Task<IEnumerable<Playlist>> GetAsync(PlaylistQueryBuilder queryBuilder)
        {
            queryBuilder.Path = PlaylistsPath;
            queryBuilder.Paged = true;

            return await GetListAsync<Playlist>(queryBuilder.BuildUri());
        }

        public async Task<SecretToken> GetSecretTokenAsync(Playlist playlist)
        {
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistSecretTokenPath, playlist.Id) };
            return await GetByIdAsync<SecretToken>(builder.BuildUri());
        }

        public async Task<IWebResult<Playlist>> PostAsync(Playlist playlist)
        {
            Validate(playlist.ValidatePost);

            var builder = new PlaylistQueryBuilder { Path = PlaylistsPath };
            return await CreateAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<IWebResult<Playlist>> UpdateAsync(Playlist playlist)
        {
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await UpdateAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<IWebResult<Playlist>> UploadArtworkAsync(Playlist playlist, Stream file)
        {
            Validate(playlist.ValidateUploadArtwork);

            var parameters = new Dictionary<string, object> { { PlaylistArtworkDataKey, file } };
            var builder = new PlaylistQueryBuilder { Path = string.Format(PlaylistPath, playlist.Id) };
            return await UpdateAsync<Playlist>(builder.BuildUri(), parameters);
        }
    }
}