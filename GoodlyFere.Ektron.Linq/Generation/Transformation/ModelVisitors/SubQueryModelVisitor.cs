#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubQueryModelVisitor.cs">
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
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Ektron.Linq.Generation.Transformation.Aggregators;
using GoodlyFere.Ektron.Linq.Generation.Transformation.Maps;
using Remotion.Linq;
using Remotion.Linq.Clauses;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Transformation.ModelVisitors
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