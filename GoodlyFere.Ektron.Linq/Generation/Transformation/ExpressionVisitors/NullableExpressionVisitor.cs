#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Helpers;
using Remotion.Linq.Parsing;
using BinaryExpression = System.Linq.Expressions.BinaryExpression;
using Expression = System.Linq.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Transformation.ExpressionVisitors
{
    internal class NullableExpressionVisitor : ExpressionTreeVisitor
    {
        #region Methods

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            if (IsEqualityComparison(expression) && IsNullComparison(expression))
            {
                return CreateBinaryNullExpression(expression);
            }

            return base.VisitBinaryExpression(expression);
        }

        protected override Expression VisitUnaryExpression(UnaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Not
                && expression.Operand is BinaryExpression
                && IsEqualityComparison(expression.Operand as BinaryExpression)
                && IsNullComparison(expression.Operand as BinaryExpression))
            {
                return CreateBinaryNullExpression(expression.Operand as BinaryExpression, true);
            }

            return base.VisitUnaryExpression(expression);
        }

        private static Expression CreateBinaryNullExpression(BinaryExpression expression, bool reverseOperation = false)
        {
            MemberExpression memberExpr = GetMemberExpression(expression);
            PropertyInfo propertyInfo = memberExpr.Member as PropertyInfo;
            PropertyExpression propertyExpr = PropertyExpressionHelper.GetPropertyExpression(memberExpr);

            ExpressionType expressionType = GetExpressionType(expression, reverseOperation);

            return new EktronBinaryNullExpression(propertyInfo.PropertyType, expressionType, propertyExpr);
        }

        private static ExpressionType GetExpressionType(BinaryExpression expression, bool reverseOperation)
        {
            ExpressionType expressionType = expression.NodeType;

            if (reverseOperation)
            {
                switch (expressionType)
                {
                    case ExpressionType.Equal:
                        expressionType = ExpressionType.NotEqual;
                        break;
                    case ExpressionType.NotEqual:
                        expressionType = ExpressionType.Equal;
                        break;
                }
            }

            return expressionType;
        }

        private static MemberExpression GetMemberExpression(BinaryExpression expression)
        {
            return new[] { expression.Left as MemberExpression, expression.Right as MemberExpression }.Single(
                e => e != null);
        }

        private static bool IsEqualityComparison(BinaryExpression expression)
        {
            return expression.NodeType == ExpressionType.Equal
                   || expression.NodeType == ExpressionType.NotEqual;
        }

        private static bool IsNullComparison(BinaryExpression expression)
        {
            return ((expression.Left is ConstantExpression && expression.Right is MemberExpression)
                    || (expression.Right is ConstantExpression && expression.Left is MemberExpression))
                   && (new[]
                       {
                           expression.Left as ConstantExpression,
                           expression.Right as ConstantExpression
                       }.Single(e => e != null).Value == null);
        }

        #endregion
    }
}