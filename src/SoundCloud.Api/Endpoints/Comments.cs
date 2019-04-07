using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Comments : IComments
    {
        private const string CommentPath = "comments/{0}?";
        private const string CommentsPath = "comments/?";

        private readonly ISoundCloudApiGateway _gateway;

        internal Comments(ISoundCloudApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<StatusResponse> DeleteAsync(Comment comment)
        {
            comment.ValidateDelete();

            var builder = new CommentsQueryBuilder { Path = string.Format(CommentPath, comment.Id) };
            return await _gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<Comment> GetAsync(int id)
        {
            var builder = new CommentsQueryBuilder { Path = string.Format(CommentPath, id) };
            return await _gateway.SendGetRequestAsync<Comment>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit, int offset = 0)
        {
            var builder = new CommentsQueryBuilder { Path = CommentsPath, Paged = true, Limit = limit, Offset = offset };
            return (await _gateway.SendGetRequestAsync<PagedResult<Comment>>(builder.BuildUri())).Collection;
        }

        public async Task<Comment> PostAsync(Comment comment)
        {
            comment.ValidatePost();

            var builder = new CommentsQueryBuilder { Path = CommentsPath };
            return await _gateway.SendPostRequestAsync<Comment>(builder.BuildUri(), comment);
        }
    }
}