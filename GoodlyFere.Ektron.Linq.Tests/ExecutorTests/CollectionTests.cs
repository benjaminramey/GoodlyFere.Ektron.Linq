#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionTests.cs">
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
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using NSubstitute;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.ExecutorTests
{
    public class CollectionTests
    {
        #region Constants and Fields

        private readonly EktronQueryExecutor _executor;
        private readonly IdProvider _idProvider;
        private readonly ISearchManager _searchManager;

        #endregion

        #region Constructors and Destructors

        public CollectionTests()
        {
            _idProvider = new IdProvider();
            _searchManager = Substitute.For<ISearchManager>();
            _executor = new EktronQueryExecutor(_idProvider, _searchManager);
        }

        #endregion

        #region Public Methods

        [Fact]
        public void ExecuteCollection_ManyResults_ReturnsMany()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider) select w;
            var model = TestHelper.GetQueryModel(query);

            _searchManager.Search(Arg.Any<AdvancedSearchCriteria>()).ReturnsForAnyArgs(
                TestHelper.GetResponseData<Widget>(10)
                );

            IEnumerable<Widget> result = _executor.ExecuteCollection<Widget>(model);

            Assert.NotNull(result);
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void ExecuteCollection_NoResults_ReturnsEmptyList()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider) select w;
            var model = TestHelper.GetQueryModel(query);

            _searchManager.Search(Arg.Any<AdvancedSearchCriteria>()).ReturnsForAnyArgs(
                TestHelper.GetResponseData<Widget>(0)
                );

            IEnumerable<Widget> result = _executor.ExecuteCollection<Widget>(model);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        #endregion
    }
}