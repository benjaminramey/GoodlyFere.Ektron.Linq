#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnaryTests.cs">
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
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.TranslationTests
{
    public class UnaryTests
    {
        #region Constants and Fields

        private readonly IdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public UnaryTests()
        {
            _idProvider = new IdProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void Not()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where !(w.Id == expectedValue)
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = !(SearchContentProperty.Id == expectedValue);
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SimpleCast_HasEktronExpressionTypeCastToObject_IgnoresCast()
        {
            object expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where (object)w.Id == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == (long)expectedValue;

            Assert.True(actualTranslation is BinaryExpression);
            Assert.True(
                (actualTranslation as BinaryExpression).Left is IntegerPropertyExpression,
                "Left is not a IntegerPropertyExpression");
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SimpleCast_NoAssignedEktronExpressionType_AllowsCast()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<CastTestWidget>(_idProvider)
                        where (long)w.NoTypeObjectId == expectedValue
                        select w;

            var propExpr = new IntegerPropertyExpression("NoTypeObjectId");

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = propExpr == expectedValue;

            Assert.True(actualTranslation is BinaryExpression);
            Assert.True(
                (actualTranslation as BinaryExpression).Left is IntegerPropertyExpression,
                "Left is not an IntegerPropertyExpression");
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void SimpleCast_StringEktronExpressionTypeCastToLong_ThrowsException()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<CastTestWidget>(_idProvider)
                        where (long)w.StringTypeObjectId == expectedValue
                        select w;

            Assert.Throws<InvalidQueryException>(() => TestHelper.GetTranslation(query));
        }

        [Fact]
        public void SimpleCast_StringEktronExpressionTypeCastToString_IgnoresCast()
        {
            string expectedValue = "1";
            var query = from w in EktronQueryFactory.Queryable<CastTestWidget>(_idProvider)
                        where (string)w.StringTypeObjectId == expectedValue
                        select w;

            var propExpr = new StringPropertyExpression("StringTypeObjectId");

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = propExpr == expectedValue;

            Assert.True(actualTranslation is BinaryExpression);
            Assert.True(
                (actualTranslation as BinaryExpression).Left is StringPropertyExpression,
                "Left is not a StringPropertyExpression");
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}