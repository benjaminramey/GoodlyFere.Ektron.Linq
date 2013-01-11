#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectFromWhereTests.cs">
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

using System.Linq;
using System;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.TranslationTests
{
    public class SelectFromWhereTests
    {
        #region Constants and Fields

        private readonly IdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public SelectFromWhereTests()
        {
            _idProvider = new IdProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void SelectFromWhere_3Clauses()
        {
            string expectedName = "anything";
            long expectedIdHigh = 10L;
            long expectedIdLow = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name == expectedName && w.Id < expectedIdHigh && w.Id > expectedIdLow
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = (new StringPropertyExpression("Name")
                                       == expectedName
                                       & SearchContentProperty.Id < expectedIdHigh)
                                      & SearchContentProperty.Id > expectedIdLow;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_4Clauses()
        {
            string expectedName = "anything";
            string expectedName2 = "nothing";
            long expectedIdHigh = 10L;
            long expectedIdLow = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name == expectedName
                              && w.Id < expectedIdHigh
                              && w.Id > expectedIdLow
                              && w.Name == expectedName2
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      == expectedName
                                      & SearchContentProperty.Id < expectedIdHigh
                                      & SearchContentProperty.Id > expectedIdLow
                                      & new StringPropertyExpression("Name")
                                      == expectedName2;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_4ClausesGrouped()
        {
            string expectedName = "anything";
            string expectedName2 = "nothing";
            long expectedIdHigh = 10L;
            long expectedIdLow = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where (w.Name == expectedName
                               && w.Id < expectedIdHigh)
                              && (w.Id > expectedIdLow
                                  && w.Name == expectedName2)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = (new StringPropertyExpression("Name")
                                       == expectedName
                                       & SearchContentProperty.Id < expectedIdHigh)
                                      & (SearchContentProperty.Id > expectedIdLow
                                         & new StringPropertyExpression("Name")
                                         == expectedName2);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_BasicProperty()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_Equals()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_EqualsReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where expectedValue == w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThan()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id > expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id > expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThanOrEqual()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id >= expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id >= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThanOrEqualReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where expectedValue <= w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id >= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_GreaterThanReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where expectedValue < w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id > expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThan()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id < expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id < expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThanOrEqual()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id <= expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id <= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThanOrEqualReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where expectedValue >= w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id <= expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LessThanReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where expectedValue > w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id < expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalAnd()
        {
            string expectedName = "anything";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name == expectedName && w.Id == expectedId
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      == expectedName
                                      & SearchContentProperty.Id == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalAndWithLogicalOr()
        {
            string expectedName = "anything";
            string expectedName2 = "something";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name != expectedName2 && w.Name == expectedName || w.Id == expectedId
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      != expectedName2
                                      & new StringPropertyExpression("Name")
                                        == expectedName
                                      | SearchContentProperty.Id == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalAndWithLogicalOrGrouped()
        {
            string expectedName = "anything";
            string expectedName2 = "something";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where (w.Name != expectedName2 && w.Name == expectedName) || w.Id == expectedId
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      != expectedName2
                                      & new StringPropertyExpression("Name")
                                        == expectedName
                                      | SearchContentProperty.Id == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalAndWithLogicalOrGrouped2()
        {
            string expectedName = "anything";
            string expectedName2 = "something";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name != expectedName2 && (w.Name == expectedName || w.Id == expectedId)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      != expectedName2
                                      & (new StringPropertyExpression("Name")
                                         == expectedName
                                         | SearchContentProperty.Id == expectedId);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_LogicalOr()
        {
            string expectedName = "anything";
            long expectedId = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name == expectedName || w.Id == expectedId
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      == expectedName
                                      | SearchContentProperty.Id == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_NotEquals()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Id != expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id != expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_NotEqualsReverse()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where expectedValue != w.Id
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id != expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_NullComparisonConstantTurnsIntoEmptyString()
        {
            string expectedValue = null;

            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name").EqualTo(expectedValue);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_StringContains()
        {
            string expectedValue = "anything";
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name.Contains(expectedValue)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                .Contains(
                    expectedValue, WordForms.Inflections);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SelectFromWhere_UndecoratedProperty()
        {
            string expectedValue = "anything";
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where w.Name == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new StringPropertyExpression("Name")
                                      == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}