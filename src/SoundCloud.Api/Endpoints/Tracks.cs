using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.Exceptions;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Tracks : ITracks
    {
        private const string TrackArtworkDataKey = "track[artwork_data]";
        private const string TrackByIdPath = "tracks/{0}?";
        private const string TrackCommentsPath = "tracks/{0}/comments?";
        private const string TrackFavoritersPath = "tracks/{0}/favoriters?";
        private const string TrackPath = "tracks?";
        private const string TrackSecretTokenPath = "tracks/{0}/secret-token?";

        private readonly ISoundCloudApiGateway _gateway;

        internal Tracks(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<StatusResponse> DeleteAsync(Track track)
        {
            track.ValidateDelete();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await _gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<Track> GetAsync(int trackId)
        {
            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, trackId) };
            return await _gateway.SendGetRequestAsync<Track>(builder.BuildUri());
        }

        public async Task<IEnumerable<Track>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0)
        {
            return await GetAllAsync(new TrackQueryBuilder { Limit = limit, Offset = offset });
        }

        public async Task<IEnumerable<Track>> GetAllAsync(TrackQueryBuilder builder)
        {
            builder.Path = TrackPath;
            builder.Paged = true;

            return (await _gateway.SendGetRequestAsync<PagedResult<Track>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(Track track, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0)
        {
            track.ValidateGet();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackCommentsPath, track.Id), Paged = true, Limit = limit, Offset = offset };
            return (await _gateway.SendGetRequestAsync<PagedResult<Comment>>(builder.BuildUri())).Collection;
        }

        public async Task<IEnumerable<User>> GetFavoritersAsync(Track track, int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0)
        {
            track.ValidateGet();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackFavoritersPath, track.Id), Paged = true, Limit = limit, Offset = offset };
            return (await _gateway.SendGetRequestAsync<PagedResult<User>>(builder.BuildUri())).Collection;
        }

        public async Task<SecretToken> GetSecretTokenAsync(Track track)
        {
            track.ValidateGet();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackSecretTokenPath, track.Id) };
            return await _gateway.SendGetRequestAsync<SecretToken>(builder.BuildUri());
        }

        public async Task<Track> UpdateAsync(Track track)
        {
            track.ValidateUpdate();

            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await _gateway.SendPutRequestAsync<Track>(builder.BuildUri(), track);
        }

        public async Task<Track> UploadArtworkAsync(Track track, Stream file)
        {
            track.ValidateUploadArtwork();

            var parameters = new Dictionary<string, object> { { TrackArtworkDataKey, file } };
            var builder = new TrackQueryBuilder { Path = string.Format(TrackByIdPath, track.Id) };
            return await _gateway.SendPutRequestAsync<Track>(builder.BuildUri(), parameters);
        }

        public async Task<Track> UploadTrackAsync(string title, Stream file)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new SoundCloudValidationException("Title must not be empty.");
            }

            var parameters = new Dictionary<string, object>();
            parameters.Add("track[title]", title);
            parameters.Add("track[asset_data]", file);

            var builder = new TrackQueryBuilder { Path = TrackPath };
            return await _gateway.SendPostRequestAsync<Track>(builder.BuildUri(), parameters);
        }
    }
}