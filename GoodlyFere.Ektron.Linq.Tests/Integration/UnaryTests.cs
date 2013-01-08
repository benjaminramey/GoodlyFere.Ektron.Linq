#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Integration
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
        public void SimpleCast()
        {
            long expectedValue = 1L;
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where (long)w.ObjectId == expectedValue
                        select w;

            var actualTranslation = TestHelper.GetTranslation(query);
            var expectedTranslation = SearchContentProperty.Id == expectedValue;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }

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

        #endregion
    }
}