#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.Cms.Search;
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

            var propExpr = new global::Ektron.Cms.Search.Expressions.IntegerPropertyExpression("Number");
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