#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Execution.Search;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Maps
{
    internal delegate T ScalarResultHandlerMethod<T>(AdvancedSearchCriteria criteria, EktronSearcher searcher);

    internal class ScalarResultMaps : Dictionary<Type, IDictionary>
    {
        #region Constructors and Destructors

        public ScalarResultMaps()
        {
            Add(typeof(Int32), new Int32ResultMap());
            Add(typeof(Int64), new Int64ResultMap());
            Add(typeof(bool), new BoolResultMap());
        }

        #endregion
    }
}