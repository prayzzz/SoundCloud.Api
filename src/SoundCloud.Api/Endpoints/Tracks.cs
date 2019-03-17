using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Tracks : Endpoint, ITracks
    {
        private const string TrackArtworkDataKey = "track[artwork_data]";
        private const string TrackByIdPath = "tracks/{0}?";
        private const string TrackCommentsPath = "tracks/{0}/comments?";
        private const string TrackFavoritersPath = "tracks/{0}/favoriters?";
        private const string TrackPath = "tracks?";
        private const string TrackSecretTokenPath = "tracks/{0}/secret-token?";

        internal Tracks(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<IWebResult> DeleteAsync(Track track)
        {
            Validate(track.ValidateDelete);

            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<Track> GetAsync(int trackId)
        {
            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, trackId) };
            return await GetByIdAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetAsync()
        {
            return await GetAsync(new TrackQueryBuilder());
        }
        
        public async Task<IEnumerable<Track>> GetAsync(SoundCloudQueryBuilder builder)
        {
            builder.Path = TrackPath;
            builder.Paged = true;

            return await GetListAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(Track track)
        {
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder { Path = string.Format(TrackCommentsPath, track.Id), Paged = true };
            return await GetListAsync<Comment>(builder.BuildUri());
        }

        public Task<IEnumerable<User>> GetFavoritersAsync(Track track)
        {
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder { Path = string.Format(TrackFavoritersPath, track.Id), Paged = true };
            return GetListAsync<User>(builder.BuildUri());
        }

        public async Task<SecretToken> GetSecretTokenAsync(Track track)
        {
            Validate(track.ValidateGet);

            var builder = new TrackQueryBuilder { Path = string.Format(TrackSecretTokenPath, track.Id) };
            return await GetByIdAsync<SecretToken>(builder.BuildUri());
        }

        public async Task<IWebResult<Track>> UpdateAsync(Track track)
        {
            Validate(track.ValidateUpdate);

            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await UpdateAsync<Track>(builder.BuildUri(), track);
        }

        public async Task<IWebResult<Track>> UploadArtworkAsync(Track track, Stream file)
        {
            Validate(track.ValidateUploadArtwork);

            var parameters = new Dictionary<string, object> { { TrackArtworkDataKey, file } };
            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await UpdateAsync<Track>(builder.BuildUri(), parameters);
        }

        public async Task<IWebResult<Track>> UploadTrackAsync(string title, Stream file)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new SoundCloudValidationException("Title must not be empty.");
            }

            var parameters = new Dictionary<string, object>();
// TODO            parameters.Add("oauth_token", Credentials.AccessToken);
            parameters.Add("track[title]", title);
            parameters.Add("track[asset_data]", file);

            var builder = new TrackQueryBuilder { Path = TrackPath };
            return await CreateAsync<Track>(builder.BuildUri(), parameters);
        }
    }
}