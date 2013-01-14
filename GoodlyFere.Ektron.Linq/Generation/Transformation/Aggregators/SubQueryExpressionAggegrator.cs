#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubQueryExpressionAggegrator.cs">
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
using System.Linq.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Transformation.Aggregators
{
    internal class SubQueryExpressionAggegrator
    {
        #region Constants and Fields

        private readonly Stack<Expression> _expressions;

        #endregion

        #region Constructors and Destructors

        public SubQueryExpressionAggegrator()
        {
            _expressions = new Stack<Expression>();
        }

        #endregion

        #region Public Methods

        public void Add(Expression expression)
        {
            _expressions.Push(expression);
        }

        public void Add(List<Expression> list)
        {
            list.ForEach(i => _expressions.Push(i));
        }

        public Expression GetExpression()
        {
            bool reverseLast = _expressions.Count % 2 != 0;
            var inOrder = new Queue<Expression>(_expressions);
            while (inOrder.Count > 1)
            {
                var right = inOrder.Dequeue();
                var left = inOrder.Dequeue();
                inOrder.Enqueue(
                    inOrder.Count == 0 && reverseLast ? Expression.OrElse(right, left) : Expression.OrElse(left, right));
            }

            return inOrder.Count > 0 ? inOrder.Dequeue() : null;
        }

        #endregion
    }
}