#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullableExpressionVisitor.cs">
// LINQ to Ektron Search, a LINQ interface to the Ektron AdvancedSearchCriteria search engine
// 
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

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

            return new EktronBinaryNullExpression(typeof(bool), expressionType, propertyExpr);
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