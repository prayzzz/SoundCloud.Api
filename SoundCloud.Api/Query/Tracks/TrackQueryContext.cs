using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using SharpSound.SoundCloud.Entities;

namespace SharpSound.SoundCloud.Query.Tracks
{
    public class TrackQueryContext
    {
        /// <summary>
        /// Executes the expression tree that is passed to it. 
        /// </summary>
        /// <param name="expression">Expression Tree</param>
        /// <param name="isEnumerable"></param>
        /// <returns></returns>
        internal static object Execute(Expression expression, bool isEnumerable)
        {
            // The expression must represent a query over the data source. 
            if (!IsQueryOverDataSource(expression))
            {
                throw new InvalidProgramException("No query over the data source was specified.");
            }

            // Find the call to Where() and get the lambda expression predicate.
            var whereFinder = new InnermostWhereFinder();
            var whereExpression = whereFinder.GetInnermostWhere(expression);
            var lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // Send the lambda expression through the partial evaluator.
            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            // Get the place name(s) to query the Web service with.
            var lf = new TrackFinder(lambdaExpression.Body);
            var locations = lf.TrackNames;
            if (locations.Count == 0)
            {
                throw new InvalidQueryException("You must specify at least one place name in your query.");
            }

            // Call the Web service and get the results.
            Track[] places = WebServiceHelper.GetTracks(locations);

            // Copy the IEnumerable places to an IQueryable.
            IQueryable<Track> queryablePlaces = places.AsQueryable();

            // Copy the expression tree that was passed in, changing only the first 
            // argument of the innermost MethodCallExpression.
            var treeCopier = new ExpressionTreeModifier(queryablePlaces);
            var newExpressionTree = treeCopier.Visit(expression);

            // This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods. 
            if (isEnumerable)
            {
                return queryablePlaces.Provider.CreateQuery(newExpressionTree);
            }

            return queryablePlaces.Provider.Execute(newExpressionTree);
        }

        private static bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance, 
            // expression is of type ConstantExpression, not MethodCallExpression. 
            return (expression is MethodCallExpression);
        }
    }

    internal class WebServiceHelper
    {
        public static Track[] GetTracks(List<string> locations)
        {
            return new[] { new Track() };
        }
    }
}