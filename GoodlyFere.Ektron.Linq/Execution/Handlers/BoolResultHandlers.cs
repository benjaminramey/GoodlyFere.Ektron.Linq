#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Execution.Search;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Handlers
{
    internal class BoolResultHandlers
    {
        #region Methods

        internal static bool HandleAny(AdvancedSearchCriteria criteria, EktronSearcher searcher)
        {
            criteria.PagingInfo.RecordsPerPage = 1;
            List<SearchResultData> searchResultData = searcher.DoSearch(criteria);
            return searchResultData.Count > 0;
        }

        #endregion
    }
}