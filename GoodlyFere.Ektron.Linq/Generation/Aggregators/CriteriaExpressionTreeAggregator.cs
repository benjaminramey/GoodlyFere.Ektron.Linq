#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Aggregators
{
    public class CriteriaExpressionTreeAggregator
    {
        #region Constants and Fields

        private readonly Stack<Expression> _expressions;

        #endregion

        #region Constructors and Destructors

        public CriteriaExpressionTreeAggregator()
        {
            _expressions = new Stack<Expression>();
        }

        #endregion

        #region Public Methods

        public void Add(Expression expression)
        {
            _expressions.Push(expression);
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
                    inOrder.Count == 0 && reverseLast
                        ? new AndExpression(right, left)
                        : new AndExpression(left, right));
            }

            return inOrder.Count > 0 ? inOrder.Dequeue() : null;
        }

        #endregion
    }
}