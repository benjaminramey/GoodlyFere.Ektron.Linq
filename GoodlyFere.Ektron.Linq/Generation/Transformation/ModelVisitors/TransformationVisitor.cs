#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransformationVisitor.cs">
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
using GoodlyFere.Ektron.Linq.Generation.Transformation.ExpressionVisitors;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Transformation.ModelVisitors
{
    public class TransformationVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly List<ExpressionTreeVisitor> _whereClauseVisitors;

        #endregion

        #region Constructors and Destructors

        public TransformationVisitor()
        {
            _whereClauseVisitors = new List<ExpressionTreeVisitor>
                {
                    new NullableExpressionVisitor(),
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