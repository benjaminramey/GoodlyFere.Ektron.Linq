#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Ektron.Cms.Search;

#endregion

namespace GoodlyFere.Ektron.Linq.Search
{
    internal class EktronSearcher
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger<EktronSearcher>();

        private readonly ISearchManager _searchManager;

        #endregion

        #region Constructors and Destructors

        public EktronSearcher(ISearchManager searchManager)
        {
            _searchManager = searchManager;
        }

        #endregion

        #region Methods

        internal List<SearchResultData> DoSearch(AdvancedSearchCriteria criteria)
        {
            List<SearchResultData> results;
            try
            {
                SearchResponseData searchResponse = _searchManager.Search(criteria);
                results = searchResponse == null ? new List<SearchResultData>() : searchResponse.Results;
            }
            catch (Exception ex)
            {
                Log.Error("Ektron SearchManager threw an exception.", ex);
                results = new List<SearchResultData>();
            }
            return results;
        }

        #endregion
    }
}