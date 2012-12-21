#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Logging;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Mappings;
using GoodlyFere.Ektron.Linq.Generation.MethodCallHandlers;
using GoodlyFere.Ektron.Linq.Helpers;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using Remotion.Linq.Clauses.ExpressionTreeVisitors;
using Remotion.Linq.Parsing;
using BinaryExpression = System.Linq.Expressions.BinaryExpression;
using Expression = Ektron.Cms.Search.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.ExpressionTreeVisitors
{
    public class QueryBuildingVisitor : ThrowingExpressionTreeVisitor
    {
        #region Constants and Fields

        private static readonly BinaryExpressionFactoryMethodMap BinaryExpressionMap =
            new BinaryExpressionFactoryMethodMap();

        private static readonly ConstantExpressionMap ConstantExpressionMap = new ConstantExpressionMap();
        private static readonly ILog Log = LogManager.GetLogger<QueryBuildingVisitor>();
        private static readonly HandledMethodsMap MethodCallMap = new HandledMethodsMap();
        private static readonly ReverseOperatorMap ReverseOperatorMap = new ReverseOperatorMap();

        private readonly Stack<Expression> _ekExpressions;

        #endregion

        #region Constructors and Destructors

        private QueryBuildingVisitor()
        {
            _ekExpressions = new Stack<Expression>();
        }

        #endregion

        #region Public Methods

        public static global::Ektron.Cms.Search.Expressions.Expression Build(System.Linq.Expressions.Expression expression)
        {
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
                    string.Format("Binary expression node type {0} not supported", expression.NodeType));
            }

            return expression;
        }

        protected override System.Linq.Expressions.Expression VisitConstantExpression(ConstantExpression expression)
        {
            object value = expression.Value;
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

        private global::Ektron.Cms.Search.Expressions.PropertyExpression GetPropertyExpression(MemberExpression expression)
        {
            var ekProp = expression.Member.GetCustomAttribute<EktronPropertyAttribute>();
            global::Ektron.Cms.Search.Expressions.PropertyExpression propExpr = ekProp != null
                                                 ? PropertyExpressionHelper.GetPropertyExpression(ekProp)
                                                 : new global::Ektron.Cms.Search.Expressions.StringPropertyExpression(expression.Member.Name);

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