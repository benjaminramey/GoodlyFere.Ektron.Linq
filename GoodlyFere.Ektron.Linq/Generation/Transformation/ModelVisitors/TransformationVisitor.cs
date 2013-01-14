#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Ektron.Linq.Generation.Transformation.ExpressionVisitors;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Transformation.ModelVisitors
{
    internal class TransformationVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly List<ExpressionTreeVisitor> _whereClauseVisitors;

        #endregion

        #region Constructors and Destructors

        public TransformationVisitor()
        {
            _whereClauseVisitors = new List<ExpressionTreeVisitor>
                {
                    new SubQueryExpressionVisitor()
                };
        }

        #endregion

        #region Public Methods

        public static void Transform(QueryModel queryModel)
        {
            var visitor = new TransformationVisitor();
            queryModel.Accept(visitor);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            foreach (var visitor in _whereClauseVisitors)
            {
                whereClause.TransformExpressions(visitor.VisitExpression);
            }

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}