#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Ektron.Linq.Execution.Handlers;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Maps
{
    internal class Int32ResultMap : Dictionary<Type, ScalarResultHandlerMethod<Int32>>
    {
        #region Constructors and Destructors

        public Int32ResultMap()
        {
            Add(typeof(CountResultOperator), Int32ResultHandlers.HandleCount);
        }

        #endregion
    }
}