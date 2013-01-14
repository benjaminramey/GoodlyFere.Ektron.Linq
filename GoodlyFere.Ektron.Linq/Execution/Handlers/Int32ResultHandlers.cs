#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Execution.Search;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Handlers
{
    internal class Int32ResultHandlers
    {
        #region Methods

        internal static Int32 HandleCount(AdvancedSearchCriteria criteria, EktronSearcher searcher)
        {
            criteria.PagingInfo.RecordsPerPage = int.MaxValue;
            List<SearchResultData> searchResultData = searcher.DoSearch(criteria);
            return searchResultData.Count;
        }

        #endregion
    }
}