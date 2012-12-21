#region Usings

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

using GoodlyFere.Ektron.Linq.Generation.Aggregators;
using GoodlyFere.Ektron.Linq.Generation.ExpressionTreeVisitors;

using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public class SubQueryModelVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly SubQueryExpressionAggegrator _aggegrator;

        private IEnumerable _values;

        #endregion

        #region Constructors and Destructors

        public SubQueryModelVisitor()
        {
            _aggegrator = new SubQueryExpressionAggegrator();
        }

        #endregion

        #region Properties

        private Expression ExpressionTree
        {
            get
            {
                return _aggegrator.GetExpression();
            }
        }

        #endregion

        #region Public Methods

        public static Expression Expand(QueryModel queryModel)
        {
            var visitor = new SubQueryModelVisitor();
            queryModel.Accept(visitor);

            return visitor.ExpressionTree;
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            if (fromClause.FromExpression is ConstantExpression)
            {
                var constantExpr = fromClause.FromExpression as ConstantExpression;
                _values = constantExpr.Value as IEnumerable;
            }

            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
        {
            if (resultOperator is ContainsResultOperator)
            {
                var cro = resultOperator as ContainsResultOperator;
                foreach (var v in _values)
                {
                    _aggegrator.Add(Expression.Equal(cro.Item, Expression.Constant(v)));
                }
            }

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}