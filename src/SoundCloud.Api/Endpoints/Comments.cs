using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoundCloud.Api.Utils;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Comments : Endpoint, IComments
    {
        private const string CommentPath = "comments/{0}?";
        private const string CommentsPath = "comments/?";

        internal Comments(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public async Task<IWebResult> DeleteAsync(Comment comment)
        {
            Validate(comment.ValidateDelete);

            var builder = new CommentsQueryBuilder { Path = string.Format(CommentPath, comment.Id) };
            return await DeleteAsync(builder.BuildUri());
        }

        public async Task<Comment> GetAsync(int commentId)
        {
            var builder = new CommentsQueryBuilder { Path = string.Format(CommentPath, commentId) };
            return await GetByIdAsync<Comment>(builder.BuildUri());
        }

        public async Task<IEnumerable<Comment>> GetAsync()
        {
            var builder = new CommentsQueryBuilder { Path = CommentsPath, Paged = true };
            return await GetListAsync<Comment>(builder.BuildUri());
        }

        public async Task<IWebResult<Comment>> PostAsync(Comment comment)
        {
            Validate(comment.ValidatePost);

            var builder = new CommentsQueryBuilder { Path = CommentsPath };
            return await CreateAsync<Comment>(builder.BuildUri(), comment);
        }
    }
}