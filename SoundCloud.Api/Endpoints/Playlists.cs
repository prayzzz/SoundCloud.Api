using System.Collections.Generic;
using System.IO;

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

        public IWebResult Delete(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateDelete);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return Delete(builder.BuildUri());
        }

        public Playlist Get(int playlistId)
        {
            EnsureClientId();

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlistId);

            return GetById<Playlist>(builder.BuildUri());
        }

        public IEnumerable<Playlist> Get(PlaylistQueryBuilder queryBuilder)
        {
            EnsureClientId();

            queryBuilder.Path = PlaylistsPath;
            queryBuilder.Paged = true;

            return GetList<Playlist>(queryBuilder.BuildUri());
        }

        public SecretToken GetSecretToken(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistSecretTokenPath, playlist.id);

            return GetById<SecretToken>(builder.BuildUri());
        }

        public IWebResult<Playlist> Post(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidatePost);

            var builder = new PlaylistQueryBuilder();
            builder.Path = PlaylistsPath;

            return Create<Playlist>(builder.BuildUri(), playlist);
        }

        public IWebResult<Playlist> Update(Playlist playlist)
        {
            EnsureToken();
            Validate(playlist.ValidateGet);

            var builder = new PlaylistQueryBuilder();
            builder.Path = string.Format(PlaylistPath, playlist.id);

            return Update<Playlist>(builder.BuildUri(), playlist);
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
    }
}