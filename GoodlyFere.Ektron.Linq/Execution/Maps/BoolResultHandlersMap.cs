#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Execution.Handlers;
using GoodlyFere.Ektron.Linq.Execution.Search;
using Remotion.Linq.Clauses.ResultOperators;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Maps
{
    internal delegate T ScalarResultHandlerMethod<T>(AdvancedSearchCriteria criteria, EktronSearcher searcher);

    internal class BoolResultHandlersMap : Dictionary<Type, ScalarResultHandlerMethod<bool>>
    {
        #region Constructors and Destructors

        public BoolResultHandlersMap()
        {
            Add(typeof(AnyResultOperator), BoolResultHandlers.HandleAny);
        }

        #endregion
    }
}