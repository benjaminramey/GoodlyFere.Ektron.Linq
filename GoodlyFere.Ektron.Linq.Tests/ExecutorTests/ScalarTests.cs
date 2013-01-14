#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScalarTests.cs">
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
    public class ScalarTests
    {
        #region Constants and Fields

        private readonly IdProvider _idProvider;
        private readonly ISearchManager _searchManager;
        private readonly SearchResponseData _testData;
        private readonly EktronQueryable<Widget> _widgets;

        #endregion

        #region Constructors and Destructors

        public ScalarTests()
        {
            _idProvider = new IdProvider();
            _testData = TestHelper.GetResponseData<Widget>(10);

            _searchManager = Substitute.For<ISearchManager>();
            _searchManager.Search(Arg.Any<AdvancedSearchCriteria>()).ReturnsForAnyArgs(
                _testData
                );

            _widgets = EktronQueryFactory.Queryable<Widget>(_idProvider, _searchManager);
        }

        #endregion

        #region Public Methods
        
        [Fact]
        public void Any()
        {
            Assert.True(_widgets.Any());
        }

        [Fact]
        public void AnyWhere()
        {
            string expectedName = (string)_testData.Results[0]["Name"];
            bool expectedResult = _testData.Results.Any(td => (string)td["Name"] == expectedName);

            Assert.Equal(expectedResult, _widgets.Any(w => w.Name == expectedName));
        }

        [Fact]
        public void Count()
        {
            Assert.Equal(_widgets.Count(), 10);
        }

        [Fact]
        public void CountLong()
        {
            Assert.Equal(_widgets.LongCount(), 10L);
        }

        [Fact]
        public void CountWhere()
        {
            string expectedName = (string)_testData.Results[0]["Name"];
            int expectedCount = _testData.Results.Count(r => (string)r["Name"] == expectedName);

            Assert.Equal(expectedCount, _widgets.Count(w => w.Name == expectedName));
        }

        [Fact]
        public void LongCountWhere()
        {
            string expectedName = (string)_testData.Results[0]["Name"];
            long expectedCount = _testData.Results.LongCount(r => (string)r["Name"] == expectedName);

            Assert.Equal(expectedCount, _widgets.LongCount(w => w.Name == expectedName));
        }

        #endregion
    }
}