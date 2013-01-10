#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Query
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

            Assert.True(actualTranslation is Ek.BinaryExpression);
            Assert.True(
                (actualTranslation as Ek.BinaryExpression).Left is Ek.IntegerPropertyExpression,
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

            var propExpr = new Ek.IntegerPropertyExpression("NoTypeObjectId");

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = propExpr == expectedValue;

            Assert.True(actualTranslation is Ek.BinaryExpression);
            Assert.True(
                (actualTranslation as Ek.BinaryExpression).Left is Ek.IntegerPropertyExpression,
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

            var propExpr = new Ek.StringPropertyExpression("StringTypeObjectId");

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = propExpr == expectedValue;

            Assert.True(actualTranslation is Ek.BinaryExpression);
            Assert.True(
                (actualTranslation as Ek.BinaryExpression).Left is Ek.StringPropertyExpression,
                "Left is not a StringPropertyExpression");
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

        #endregion
    }
}