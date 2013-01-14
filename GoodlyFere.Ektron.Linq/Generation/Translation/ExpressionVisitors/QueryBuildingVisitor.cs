#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryBuildingVisitor.cs">
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Logging;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Translation.Maps;
using GoodlyFere.Ektron.Linq.Helpers;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;
using BinaryExpression = System.Linq.Expressions.BinaryExpression;
using Expression = Ektron.Cms.Search.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.ExpressionVisitors
{
    public class QueryBuildingVisitor : ThrowingExpressionTreeVisitor
    {
        #region Constants and Fields

        private static readonly BinaryExpressionMap BinaryExpressionMap =
            new BinaryExpressionMap();

        private static readonly ConstantExpressionMap ConstantExpressionMap = new ConstantExpressionMap();
        private static readonly ILog Log = LogManager.GetLogger<QueryBuildingVisitor>();
        private static readonly ObjectMethodsMap MethodCallMap = new ObjectMethodsMap();
        private static readonly ReverseOperatorMap ReverseOperatorMap = new ReverseOperatorMap();

        private static readonly UnaryExpressionMap UnaryExpressionMap =
            new UnaryExpressionMap();

        private readonly Stack<Expression> _ekExpressions;

        #endregion

        #region Constructors and Destructors

        private QueryBuildingVisitor()
        {
            _ekExpressions = new Stack<Expression>();
        }

        #endregion

        #region Public Methods

        public static global::Ektron.Cms.Search.Expressions.Expression Build(
            System.Linq.Expressions.Expression expression)
        {
            Log.DebugFormat("Visiting expression: {0}", FormattingExpressionTreeVisitor.Format(expression));

            var visitor = new QueryBuildingVisitor();
            visitor.VisitExpression(expression);

            return visitor._ekExpressions.Pop();
        }

        #endregion

        #region Methods

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            var itemAsExpression = unhandledItem as System.Linq.Expressions.Expression;
            string formatted = itemAsExpression == null
                                   ? unhandledItem.ToString()
                                   : FormattingExpressionTreeVisitor.Format(itemAsExpression);

            Log.ErrorFormat("Unhandled expression: {0} ", formatted);
            return new Exception("I can't handle it! Expression type: " + typeof(T).Name);
        }

        protected override System.Linq.Expressions.Expression VisitBinaryExpression(BinaryExpression expression)
        {
            VisitExpression(expression.Left);
            global::Ektron.Cms.Search.Expressions.Expression leftExpr = _ekExpressions.Pop();

            VisitExpression(expression.Right);
            global::Ektron.Cms.Search.Expressions.Expression rightExpr = _ekExpressions.Pop();

            global::Ektron.Cms.Search.Expressions.Expression propExpr;
            global::Ektron.Cms.Search.Expressions.Expression valueExpr;
            ExpressionType operatorType;
            ReorderBinaryExpressions(
                expression.NodeType, leftExpr, rightExpr, out propExpr, out valueExpr, out operatorType);

            if (BinaryExpressionMap.ContainsKey(operatorType))
            {
                var factoryMethod = BinaryExpressionMap[operatorType];
                _ekExpressions.Push(factoryMethod.Invoke(propExpr, valueExpr));
            }
            else
            {
                throw new NotSupportedException(
                    string.Format("Binary expression node type {0} not supported in Ektron.", expression.NodeType));
            }

            return expression;
        }

        protected override System.Linq.Expressions.Expression VisitConstantExpression(ConstantExpression expression)
        {
            object value = expression.Value ?? string.Empty;
            global::Ektron.Cms.Search.Expressions.Expression valueExpression;
            if (ConstantExpressionMap.ContainsKey(value.GetType()))
            {
                valueExpression = ConstantExpressionMap[value.GetType()].Invoke(value);
            }
            else
            {
                return base.VisitConstantExpression(expression);
            }

            _ekExpressions.Push(valueExpression);

            return expression;
        }

        protected override System.Linq.Expressions.Expression VisitMemberExpression(MemberExpression expression)
        {
            global::Ektron.Cms.Search.Expressions.PropertyExpression propExr = GetPropertyExpression(expression);
            _ekExpressions.Push(propExr);

            return expression;
        }

        protected override System.Linq.Expressions.Expression VisitMethodCallExpression(MethodCallExpression expression)
        {
            if (MethodCallMap.ContainsKey(expression.Method))
            {
                var handler = MethodCallMap[expression.Method];
                var methodCallExpr = handler.Invoke(expression.Object, expression.Arguments);
                _ekExpressions.Push(methodCallExpr);

                return expression;
            }

            return base.VisitMethodCallExpression(expression);
        }

        protected override System.Linq.Expressions.Expression VisitUnaryExpression(UnaryExpression expression)
        {
            VisitExpression(expression.Operand);
            global::Ektron.Cms.Search.Expressions.Expression operand = _ekExpressions.Pop();

            ExpressionType operatorType = expression.NodeType;
            if (UnaryExpressionMap.ContainsKey(operatorType))
            {
                var factoryMethod = UnaryExpressionMap[operatorType];
                _ekExpressions.Push(factoryMethod.Invoke(expression, operand));
            }
            else
            {
                return base.VisitUnaryExpression(expression);
            }

            return expression;
        }

        private global::Ektron.Cms.Search.Expressions.PropertyExpression GetPropertyExpression(
            MemberExpression expression)
        {
            var ekProp = expression.Member.GetCustomAttribute<EktronPropertyAttribute>();
            global::Ektron.Cms.Search.Expressions.PropertyExpression propExpr = ekProp != null
                                                                                    ? PropertyExpressionHelper
                                                                                          .GetPropertyExpression(ekProp)
                                                                                    : new global::Ektron.Cms.Search.
                                                                                          Expressions.
                                                                                          StringPropertyExpression(
                                                                                          expression.Member.Name);

            return propExpr;
        }

        private void ReorderBinaryExpressions(
            ExpressionType nodeType,
            global::Ektron.Cms.Search.Expressions.Expression leftExpr,
            global::Ektron.Cms.Search.Expressions.Expression rightExpr,
            out global::Ektron.Cms.Search.Expressions.Expression propExpr,
            out global::Ektron.Cms.Search.Expressions.Expression valueExpr,
            out ExpressionType operatorType)
        {
            propExpr = leftExpr;
            valueExpr = rightExpr;
            bool leftIsPropertyExpr = leftExpr is global::Ektron.Cms.Search.Expressions.PropertyExpression;
            bool rightIsPropertyExpr = rightExpr is global::Ektron.Cms.Search.Expressions.PropertyExpression;

            if (leftIsPropertyExpr && rightIsPropertyExpr)
            {
                throw new InvalidQueryException(
                    "Ektron is limited to having a PropertyExpression on the left and a ValueExpression on the right.");
            }

            operatorType = nodeType;
            // the property expression must be on the left or on both sides
            // for an advanced search criteria binary expression to parse correctly
            if (!leftIsPropertyExpr && rightIsPropertyExpr)
            {
                propExpr = rightExpr;
                valueExpr = leftExpr;

                if (ReverseOperatorMap.ContainsKey(operatorType))
                {
                    operatorType = ReverseOperatorMap[operatorType];
                }
            }
        }

        #endregion
    }
}