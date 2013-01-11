#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubQueryTests.cs">
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
using Ektron.Cms;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Query
{
    public class SubQueryTests
    {
        #region Constants and Fields

        private readonly IEktronIdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public SubQueryTests()
        {
            _idProvider = new IdProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void IEnumerableContains()
        {
            int[] numbers = new[] { 1, 2, 3, 4 };
            var query = from w in EktronQueryFactory.Queryable<NumberTestWidget>(_idProvider)
                        where numbers.Contains(w.Number)
                        select w;

            var propExpr = new IntegerPropertyExpression("Number");
            var model = TestHelper.GetQueryModel(query);

            var actualCriteria =
                new EktronQueryExecutor(_idProvider, ObjectFactory.GetSearchManager()).BuildCriteria(model);
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = (propExpr == numbers[0]
                                      | propExpr == numbers[1])
                                     | (propExpr == numbers[2]
                                        | propExpr == numbers[3])
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        [Fact]
        public void IEnumerableContains_DifferingSurfaceTypes()
        {
            long[] numbers = new long[] { 1, 2, 3, 4 };
            IEnumerable<object> objectNumbers = numbers.Select(i => i).Cast<object>();
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where objectNumbers.Contains(w.Id)
                        select w;

            var propExpr = SearchContentProperty.Id;
            var model = TestHelper.GetQueryModel(query);

            var actualCriteria =
                new EktronQueryExecutor(_idProvider, ObjectFactory.GetSearchManager()).BuildCriteria(model);
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = (propExpr == numbers[0]
                                      | propExpr == numbers[1])
                                     | (propExpr == numbers[2]
                                        | propExpr == numbers[3])
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        #endregion
    }
}