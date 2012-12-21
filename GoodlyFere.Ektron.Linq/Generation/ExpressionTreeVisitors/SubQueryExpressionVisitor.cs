#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.ExpressionTreeVisitors
{
    internal class SubQueryExpressionVisitor : ExpressionTreeVisitor
    {
        #region Methods

        protected override Expression VisitSubQueryExpression(SubQueryExpression expression)
        {
            return SubQueryModelVisitor.Expand(expression.QueryModel);
        }

        #endregion
    }
}