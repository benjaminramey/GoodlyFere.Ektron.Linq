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
    internal delegate List<Expression> SubQueryMethodCallHandler(ResultOperatorBase resultOperator, IEnumerable values);

    internal class ResultOperatorHandlersMap : Dictionary<Type, SubQueryMethodCallHandler>
    {
        #region Constructors and Destructors

        public ResultOperatorHandlersMap()
        {
            Add(typeof(ContainsResultOperator), IEnumerableMethodHandlers.HandleContains);
        }

        #endregion
    }
}