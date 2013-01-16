#region Usings

using System;
using System.Linq;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.ModelVisitors
{
    internal class WhereClauseVisitor<T> : QueryModelVisitorBase
    {
        private readonly ExpressionTreeVisitor _visitor;
        private T _result;

        #region Constructors and Destructors

        public WhereClauseVisitor(ExpressionTreeVisitor whereExpressionVisitor)
        {
            _visitor = whereExpressionVisitor;
        }
        
        #endregion

        #region Public Methods

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            _result = _visitor.VisitExpression(whereClause.Predicate);

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}