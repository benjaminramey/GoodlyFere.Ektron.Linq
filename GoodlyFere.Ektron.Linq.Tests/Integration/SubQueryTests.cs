#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using Xunit;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Integration
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
            var query = from w in EktronQueryFactory.Queryable<Widget>(_idProvider)
                        where numbers.Contains(w.Number)
                        select w;

            var propExpr = new Ek.IntegerPropertyExpression("Number");
            var model = TestHelper.GetQueryModel(query);

            var actualCriteria = new EktronQueryExecutor(_idProvider).BuildCriteria(model);
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = propExpr == numbers[0]
                                     | propExpr == numbers[1]
                                     | propExpr == numbers[2]
                                     | propExpr == numbers[3]
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        #endregion
    }
}