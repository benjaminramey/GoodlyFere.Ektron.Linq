#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullableTests.cs">
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
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.TranslationTests
{
    public class NullableTests
    {
        #region Public Methods

        [Fact]
        public void NullableBoolean_HasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableBoolean.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new BooleanPropertyExpression("NullableBoolean").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableBoolean_NotHasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where !w.NullableBoolean.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new BooleanPropertyExpression("NullableBoolean").IsNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableDateTime_HasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableDateTime.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DatePropertyExpression("NullableDateTime").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableDateTime_NotHasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where !w.NullableDateTime.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DatePropertyExpression("NullableDateTime").IsNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableDecimal_HasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableDecimal.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DecimalPropertyExpression("NullableDecimal").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableDecimal_NotHasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where !w.NullableDecimal.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DecimalPropertyExpression("NullableDecimal").IsNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableLong_HasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableLong.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableLong_NotHasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where !w.NullableLong.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong").IsNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void Nullable_Equals()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableLong == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong").EqualTo(expectedValue);

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void Nullable_EqualsNull()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableLong == null
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong").IsNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void Nullable_HasValue2_And()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableDecimal.HasValue && w.NullableDecimal2.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new BooleanPropertyExpression("NullableDecimal").IsNotNull()
                                      & new BooleanPropertyExpression("NullableDecimal2").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void Nullable_HasValue_And()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableBoolean.HasValue && w.NullableBoolean2.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new BooleanPropertyExpression("NullableBoolean").IsNotNull()
                                      & new BooleanPropertyExpression("NullableBoolean2").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void Nullable_NotEqual()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableLong != expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong") != expectedValue;

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void Nullable_NotEqualsNull()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableLong != null
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}