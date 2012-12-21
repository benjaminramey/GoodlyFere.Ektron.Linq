#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;

using Remotion.Linq;
using Remotion.Linq.Clauses;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public class SubQueryModelVisitor : QueryModelVisitorBase
    {
        #region Constructors and Destructors

        private SubQueryModelVisitor()
        {
        }

        #endregion

        #region Public Methods

        public static Expression Expand(QueryModel queryModel)
        {
            var visitor = new SubQueryModelVisitor();
            queryModel.Accept(visitor);

            return Expression.Constant(1);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}