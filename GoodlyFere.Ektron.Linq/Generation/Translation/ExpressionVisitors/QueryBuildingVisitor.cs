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
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Generation.Translation.Maps;
using GoodlyFere.Ektron.Linq.Helpers;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;
using BinaryExpression = System.Linq.Expressions.BinaryExpression;
using EktronExpression = Ektron.Cms.Search.Expressions.Expression;
using Expression = System.Linq.Expressions.Expression;

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

        private readonly Stack<EktronExpression> _ekExpressions;
        private readonly List<PropertyExpression> _propertiesUsed;

        #endregion

        #region Constructors and Destructors

        private QueryBuildingVisitor()
        {
            _ekExpressions = new Stack<EktronExpression>();
            _propertiesUsed = new List<PropertyExpression>();
        }

        #endregion

        #region Public Methods

        public static QueryBuildResult Build(Expression expression)
        {
            Log.DebugFormat("Visiting expression: {0}", FormattingExpressionTreeVisitor.Format(expression));

            var visitor = new QueryBuildingVisitor();
            visitor.VisitExpression(expression);

            var result = new QueryBuildResult
                {
                    Expression = visitor._ekExpressions.Pop(),
                    PropertiesUsed = visitor._propertiesUsed.ToArray()
                };
            return result;
        }

        #endregion

        #region Methods

        protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
        {
            var itemAsExpression = unhandledItem as Expression;
            string formatted = itemAsExpression == null
                                   ? unhandledItem.ToString()
                                   : FormattingExpressionTreeVisitor.Format(itemAsExpression);

            Log.ErrorFormat("Unhandled expression: {0} ", formatted);
            return new Exception("I can't handle it! Expression type: " + typeof(T).Name);
        }

        protected override Expression VisitBinaryExpression(BinaryExpression expression)
        {
            VisitExpression(expression.Left);
            EktronExpression leftExpr = _ekExpressions.Pop();

            VisitExpression(expression.Right);
            EktronExpression rightExpr = _ekExpressions.Pop();

            EktronExpression propExpr;
            EktronExpression valueExpr;
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

        protected override Expression VisitConstantExpression(ConstantExpression expression)
        {
            object value = expression.Value ?? string.Empty;
            EktronExpression valueExpression;
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

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            PropertyExpression propExpr = PropertyExpressionHelper.GetPropertyExpression(expression);
            _ekExpressions.Push(propExpr);
            _propertiesUsed.Add(propExpr);

            return expression;
        }

        protected override Expression VisitMethodCallExpression(MethodCallExpression expression)
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

        protected override Expression VisitUnaryExpression(UnaryExpression expression)
        {
            VisitExpression(expression.Operand);
            EktronExpression operand = _ekExpressions.Pop();

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

        private void ReorderBinaryExpressions(
            ExpressionType nodeType,
            EktronExpression leftExpr,
            EktronExpression rightExpr,
            out EktronExpression propExpr,
            out EktronExpression valueExpr,
            out ExpressionType operatorType)
        {
            propExpr = leftExpr;
            valueExpr = rightExpr;
            bool leftIsPropertyExpr = leftExpr is PropertyExpression;
            bool rightIsPropertyExpr = rightExpr is PropertyExpression;

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