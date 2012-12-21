#region Usings

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Ektron.Linq.Generation.Aggregators;
using GoodlyFere.Ektron.Linq.Generation.MethodCallHandlers.SubQueryHandlers;
using Remotion.Linq;
using Remotion.Linq.Clauses;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public class SubQueryModelVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private static readonly ResultOperatorHandlersMap ResultOperatorHandlers = new ResultOperatorHandlersMap();
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
            if (ResultOperatorHandlers.ContainsKey(resultOperator.GetType()))
            {
                var method = ResultOperatorHandlers[resultOperator.GetType()];
                _aggegrator.Add(method.Invoke(resultOperator, _values));
            }

            base.VisitResultOperator(resultOperator, queryModel, index);
        }

        #endregion
    }
}