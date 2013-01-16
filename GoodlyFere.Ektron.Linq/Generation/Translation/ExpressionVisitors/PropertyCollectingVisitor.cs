#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Helpers;
using Remotion.Linq.Parsing;
using Expression = System.Linq.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.ExpressionVisitors
{
    public class PropertyCollectingVisitor : ExpressionTreeVisitor
    {
        #region Constants and Fields

        private readonly List<string> _properties;

        #endregion

        #region Constructors and Destructors

        public PropertyCollectingVisitor()
        {
            _properties = new List<string>();
        }

        #endregion

        #region Public Methods

        public static string[] Collect(Expression expression)
        {
            var visitor = new PropertyCollectingVisitor();
            visitor.VisitExpression(expression);
            return visitor._properties.ToArray();
        }

        #endregion

        #region Methods

        protected override Expression VisitMemberExpression(MemberExpression expression)
        {
            PropertyExpression expr = PropertyExpressionHelper.GetPropertyExpression(expression);
            _properties.Add(expr.Name);

            return base.VisitMemberExpression(expression);
        }

        #endregion
    }
}