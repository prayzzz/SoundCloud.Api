using System.Threading.Tasks;
using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Comments : Endpoint, IComments
    {
        private const string CommentPath = "comments/{0}?";
        private const string CommentsPath = "comments/?";

        internal Comments(ISoundCloudApiGateway gateway) : base(gateway)
        {
        }

        public async Task<StatusResponse> DeleteAsync(Comment comment)
        {
            comment.ValidateDelete();

            var builder = new CommentsQueryBuilder { Path = string.Format(CommentPath, comment.Id) };
            return await Gateway.SendDeleteRequestAsync<StatusResponse>(builder.BuildUri());
        }

        public async Task<Comment> GetAsync(long id)
        {
            var builder = new CommentsQueryBuilder { Path = string.Format(CommentPath, id) };
            return await Gateway.SendGetRequestAsync<Comment>(builder.BuildUri());
        }

        public Task<SoundCloudList<Comment>> GetAllAsync(int limit = SoundCloudQueryBuilder.MaxLimit)
        {
            var builder = new CommentsQueryBuilder { Path = CommentsPath, Paged = true, Limit = limit };
            return GetPage<Comment>(builder.BuildUri());
        }

        public async Task<Comment> PostAsync(Comment comment)
        {
            comment.ValidatePost();

            var builder = new CommentsQueryBuilder { Path = CommentsPath };
            return await Gateway.SendPostRequestAsync<Comment>(builder.BuildUri(), comment);
        }
    }
}
