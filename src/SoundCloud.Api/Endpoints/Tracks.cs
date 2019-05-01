using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

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

        internal Tracks(ISoundCloudApiGateway gateway) : base(gateway)
        {
        }

        public async Task<StatusResponse> DeleteAsync(Track track)
        {
            track.ValidateDelete();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await Gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<Track> GetAsync(long trackId)
        {
            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, trackId) };
            return await Gateway.SendGetRequestAsync<Track>(builder.BuildUri());
        }

        public async Task<SoundCloudList<Track>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            return await GetAllAsync(new TrackQueryBuilder { Limit = limit });
        }

        public Task<SoundCloudList<Track>> GetAllAsync(TrackQueryBuilder builder)
        {
            builder.Path = TrackPath;
            builder.Paged = true;

            return GetPage<Track>(builder.BuildUri());
        }

        public Task<SoundCloudList<Comment>> GetCommentsAsync(Track track, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            track.ValidateGet();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackCommentsPath, track.Id), Paged = true, Limit = limit };
            return GetPage<Comment>(builder.BuildUri());
        }

        public Task<SoundCloudList<User>> GetFavoritersAsync(Track track, int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            track.ValidateGet();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackFavoritersPath, track.Id), Paged = true, Limit = limit };
            return GetPage<User>(builder.BuildUri());
        }

        public async Task<SecretToken> GetSecretTokenAsync(Track track)
        {
            track.ValidateGet();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackSecretTokenPath, track.Id) };
            return await Gateway.SendGetRequestAsync<SecretToken>(builder.BuildUri());
        }

        public async Task<Track> UpdateAsync(Track track)
        {
            track.ValidateUpdate();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await Gateway.SendPutRequestAsync<Track>(builder.BuildUri(), track);
        }

        public async Task<Track> UploadArtworkAsync(Track track, Stream file)
        {
            track.ValidateUploadArtwork();

            var parameters = new Dictionary<string, object> { { TrackArtworkDataKey, file } };
            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await Gateway.SendPutRequestAsync<Track>(builder.BuildUri(), parameters);
        }

        public async Task<Track> UploadTrackAsync(string title, Stream file)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new SoundCloudValidationException("Title must not be empty.");
            }

            var parameters = new Dictionary<string, object> { { "track[title]", title }, { "track[asset_data]", file } };

            var builder = new TrackQueryBuilder { Path = TrackPath };
            return await Gateway.SendPostRequestAsync<Track>(builder.BuildUri(), parameters);
        }
    }
}
