using System;
using System.Linq.Expressions;

namespace SharpSound.SoundCloud.Query
{
    internal class ExpressionTreeHelpers
    {
        internal static bool IsMemberEqualsValueExpression(Expression exp, Type declaringType, string memberName)
        {
            if (!IsValidEqualsValueExpression(exp, declaringType, memberName))
            {
                return false;
            }

            var be = (BinaryExpression)exp;

            if (IsSpecificMemberExpression(be.Left, declaringType, memberName) || IsSpecificMemberExpression(be.Right, declaringType, memberName))
            {
                return true;
            }

            if (be.Left.NodeType == ExpressionType.Convert)
            {
                var op = ((UnaryExpression)be.Left).Operand;
                return IsSpecificMemberExpression(op, declaringType, memberName);
            }

            if (be.Right.NodeType == ExpressionType.Convert)
            {
                var op = ((UnaryExpression)be.Right).Operand;
                return IsSpecificMemberExpression(op, declaringType, memberName);
            }

            return false;
        }

        internal static bool IsSpecificMemberExpression(Expression exp, Type declaringType, string memberName)
        {
            if ((exp) is MemberExpression)
            {
                return ((MemberExpression)exp).Member.DeclaringType == declaringType && ((MemberExpression)exp).Member.Name == memberName;
            }

            return false;
        }

        private static bool IsValidEqualsValueExpression(Expression exp, Type declaringType, string memberName)
        {
            if (exp.NodeType != ExpressionType.Equal)
            {
                return false;
            }

            var be = (BinaryExpression)exp;

            if (IsSpecificMemberExpression(be.Left, declaringType, memberName) &&
                IsSpecificMemberExpression(be.Right, declaringType, memberName))
            {
                throw new Exception("Cannot have 'member' == 'member' in an expression!");
            }

            return true;
        }

        internal static T GetValueFromEqualsExpression<T>(BinaryExpression be, Type memberDeclaringType, string memberName)
        {
            if (be.NodeType != ExpressionType.Equal)
            {
                throw new Exception("There is a bug in this program.");
            }

            if (IsMemberExpressionType(be.Left))
            {
                return GetValueFromExpression<T>(be.Right);
            }

            if (IsMemberExpressionType(be.Right))
            {
                return GetValueFromExpression<T>(be.Left);
            }

            // We should have returned by now. 
            throw new Exception("There is a bug in this program.");
        }

        private static bool IsMemberExpressionType(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return true;
            }

            if (expression.NodeType == ExpressionType.Convert)
            {
                return true;
            }

            return false;
        }

        internal static T GetValueFromExpression<T>(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
            {
                return (T)(((ConstantExpression)expression).Value);
            }

            throw new InvalidQueryException(String.Format("The expression type {0} is not supported to obtain a value.", expression.NodeType));
        }
    }
}