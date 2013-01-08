#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.MethodCallHandlers.SubQueryHandlers
{
    internal class IEnumerableMethodHandlers
    {
        #region Public Methods

        public static List<Expression> HandleContains(ResultOperatorBase resultOperator, IEnumerable values)
        {
            List<Expression> expressions = new List<Expression>();

            if (resultOperator is ContainsResultOperator && values != null)
            {
                var cro = resultOperator as ContainsResultOperator;
                foreach (var v in values)
                {
                    expressions.Add(
                        Expression.Equal(cro.Item, Expression.Convert(Expression.Constant(v), cro.Item.Type)));
                }
            }

            return expressions;
        }

        #endregion
    }
}