#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Ektron.Linq.Execution.Handlers;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Maps
{
    internal class Int64ResultMap : Dictionary<Type, ScalarResultHandlerMethod<Int64>>
    {
        #region Constructors and Destructors

        public Int64ResultMap()
        {
            Add(typeof(LongCountResultOperator), Int64ResultHandlers.HandleLongCount);
        }

        #endregion
    }
}