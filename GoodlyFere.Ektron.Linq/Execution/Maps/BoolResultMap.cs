#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Ektron.Linq.Execution.Handlers;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Maps
{
    internal class BoolResultMap : Dictionary<Type, ScalarResultHandlerMethod<bool>>
    {
        #region Constructors and Destructors

        public BoolResultMap()
        {
            Add(typeof(AnyResultOperator), BoolResultHandlers.HandleAny);
        }

        #endregion
    }
}