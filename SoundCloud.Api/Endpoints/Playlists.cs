using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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

        public IWebResult Delete(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateDelete);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return Delete(builder.BuildUri());
        }

        public async Task<IWebResult> DeleteAsync(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateDelete);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return await DeleteAsync(builder.BuildUri());
        }

        public Playlist Get(int playlistId)
        {
            EnsureClientId();

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlistId);

            return GetById<Playlist>(builder.BuildUri());
        }

        public async Task<Playlist> GetAsync(int playlistId)
        {
            EnsureClientId();

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlistId);

            return await GetByIdAsync<Playlist>(builder.BuildUri());
        }

        public IEnumerable<Playlist> Get(PlaylistQueryBuilder queryBuilder)
        {
            EnsureClientId();

            queryBuilder.Path = PlaylistsPath;
            queryBuilder.Paged = true;

            return GetList<Playlist>(queryBuilder.BuildUri());
        }

        public async Task<IEnumerable<Playlist>> GetAsync(PlaylistQueryBuilder queryBuilder)
        {
            EnsureClientId();

            queryBuilder.Path = PlaylistsPath;
            queryBuilder.Paged = true;

            return await GetListAsync<Playlist>(queryBuilder.BuildUri());
        }

        public SecretToken GetSecretToken(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistSecretTokenPath, playlist.id);

            return GetById<SecretToken>(builder.BuildUri());
        }

        public async Task<SecretToken> GetSecretTokenAsync(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistSecretTokenPath, playlist.id);

            return await GetByIdAsync<SecretToken>(builder.BuildUri());
        }

        public IWebResult<Playlist> Post(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidatePost);

            var builder = new PlaylistQueryBuilder();
            builder.Path = PlaylistsPath;

            return Create<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<IWebResult<Playlist>> PostAsync(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidatePost);

            var builder = new PlaylistQueryBuilder();
            builder.Path = PlaylistsPath;

            return await CreateAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public IWebResult<Playlist> Update(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return Update<Playlist>(builder.BuildUri(), playlist);
        }

        public async Task<IWebResult<Playlist>> UpdateAsync(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return await UpdateAsync<Playlist>(builder.BuildUri(), playlist);
        }

        public IWebResult<Playlist> UploadArtwork(Playlist playlist, Stream file)
        {
            EnsureToken();
            Validate(playlist.ValidateUploadArtwork);

            var parameters = new Dictionary<string, object>();
            parameters.Add(PlaylistArtworkDataKey, file);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return Update<Playlist>(builder.BuildUri(), parameters);
        }

        public async Task<IWebResult<Playlist>> UploadArtworkAsync(Playlist playlist, Stream file)
        {
            EnsureToken();
            Validate(playlist.ValidateUploadArtwork);

            var parameters = new Dictionary<string, object>();
            parameters.Add(PlaylistArtworkDataKey, file);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return await UpdateAsync<Playlist>(builder.BuildUri(), parameters);
        }
    }
}