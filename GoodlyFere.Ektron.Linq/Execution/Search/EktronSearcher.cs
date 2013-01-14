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

#endregion

namespace GoodlyFere.Ektron.Linq.Execution.Search
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