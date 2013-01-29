#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnPropertiesTests.cs">
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
    public class ReturnPropertiesTests
    {
        #region Constants and Fields

        private readonly TestableExecutor _executor;
        private readonly IdProvider _idProvider;
        private readonly ISearchManager _searchManager;

        #endregion

        #region Constructors and Destructors

        public ReturnPropertiesTests()
        {
            _idProvider = new IdProvider();
            _searchManager = Substitute.For<ISearchManager>();
            _executor = new TestableExecutor(_idProvider, _searchManager);
        }

        #endregion

        #region Public Methods

        [Fact]
        public void IncludesAllEktronPropertyAttributes()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id == 1
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = _executor.BuildCriteria<Widget>(model, 1000);

            Assert.Equal(1, criteria.ReturnProperties.Count);
            Assert.Equal("ContentId".ToLower(), criteria.ReturnProperties.First().Name);
        }

        [Fact]
        public void NoAttributesOnProperties()
        {
            var query = from w in EktronQueryFactory.Queryable<NoAttributesWidget>()
                        where w.ContentId == 1
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = _executor.BuildCriteria<NoAttributesWidget>(model, 1000);

            Assert.True(criteria.ReturnProperties.Count > 0, "0 return properties found");
            Assert.Equal("ContentId", criteria.ReturnProperties.First().Name);
        }

        #endregion
    }
}