#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EktronSearcher.cs">
// LINQ to Ektron Search, a LINQ interface to the Ektron AdvancedSearchCriteria search engine
// 
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Extensions;

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Search
{
    public class EktronSearcher
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

        #region Public Methods

        /// <summary>
        ///     Submits the given AdvancedSearchCriteria to the
        ///     Ektron ISearchManager.
        /// </summary>
        /// <remarks>
        ///     If an exception is thrown (which happens for a variety of reasons) by the
        ///     Ektron ISearchManager, the exception is logged with an
        ///     Error severity and an empty list is return.
        /// </remarks>
        /// <param name="criteria">Criteria to search Ektron with.</param>
        /// <returns>List of SearchResultData returned by Ektron ISearchManager.</returns>
        public List<SearchResultData> DoSearch(AdvancedSearchCriteria criteria)
        {
            List<SearchResultData> results;
            try
            {
                Log.DebugFormat("Searching with expression: {0}", criteria.ExpressionTree.ToFormattedString());
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