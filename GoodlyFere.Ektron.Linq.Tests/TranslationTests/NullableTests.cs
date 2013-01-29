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
            var expectedTranslation = new DecimalPropertyExpression("NullableBoolean").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableBoolean_NotHasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where !w.NullableBoolean.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DecimalPropertyExpression("NullableBoolean").IsNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableDateTime_HasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableDateTime.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DecimalPropertyExpression("NullableDateTime").IsNotNull();

            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        [Fact]
        public void NullableDateTime_NotHasValue()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where !w.NullableDateTime.HasValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new DecimalPropertyExpression("NullableDateTime").IsNull();

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
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableLong == null
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = new IntegerPropertyExpression("NullableLong").IsNull();

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
            long expectedValue = 1L;
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