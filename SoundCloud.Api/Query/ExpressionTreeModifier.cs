using System.Linq;
using System.Linq.Expressions;

using SharpSound.SoundCloud.Entities;

namespace SharpSound.SoundCloud.Query
{
    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private readonly IQueryable<Track> queryablePlaces;

        internal ExpressionTreeModifier(IQueryable<Track> places)
        {
            this.queryablePlaces = places;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (c.Type == typeof(QueryableSoundCloudData<Track>))
            {
                return Expression.Constant(this.queryablePlaces);
            }
            return c;
        }
    }
}