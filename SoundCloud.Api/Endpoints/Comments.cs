using System.Collections.Generic;

using SoundCloud.Api.Entities;
using SoundCloud.Api.QueryBuilders;
using SoundCloud.Api.Web;

namespace SoundCloud.Api.Endpoints
{
    internal sealed class Comments : Endpoint, IComments
    {
        private const string CommentPath = "comments/{0}?";
        private const string CommentsPath = "comments?";

        internal Comments(ISoundCloudApiGateway gateway)
            : base(gateway)
        {
        }

        public IWebResult Delete(Comment comment)
        {
            EnsureToken();
            Validate(comment.ValidateDelete);

            var builder = new CommentsQueryBuilder();
            builder.Path = string.Format(CommentPath, comment.id);

            return Delete(builder.BuildUri());
        }

        public Comment Get(int commentId)
        {
            EnsureClientId();

            var builder = new CommentsQueryBuilder();
            builder.Path = string.Format(CommentPath, commentId);

            return GetById<Comment>(builder.BuildUri());
        }

        public IEnumerable<Comment> Get()
        {
            EnsureClientId();

            var builder = new CommentsQueryBuilder();
            builder.Path = CommentsPath;
            builder.Paged = true;

            return GetList<Comment>(builder.BuildUri());
        }

        public IWebResult<Comment> Post(Comment comment)
        {
            EnsureToken();
            Validate(comment.ValidatePost);

            var builder = new CommentsQueryBuilder();
            builder.Path = CommentsPath;

            return Create<Comment>(builder.BuildUri(), comment);
        }
    }
}