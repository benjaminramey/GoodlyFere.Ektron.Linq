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
    public class SelectFromOrderByTests
    {
        #region Constants and Fields

        private readonly IdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public SelectFromOrderByTests()
        {
            _idProvider = new IdProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void SelectFromOrderBy()
        {
            var expectedId = _idProvider.GetSmartFormId(SmartFormWidget.SmartFormName);

            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>(_idProvider)
                        orderby w.Id
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = new EktronQueryExecutor(_idProvider).BuildCriteria(model);

            var actualTranslation = criteria.ExpressionTree;
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedId;
            EkAssert.Equal(expectedTranslation, actualTranslation);

            Assert.Equal(1, criteria.OrderBy.Count);
            Assert.Equal(SearchContentProperty.Id, criteria.OrderBy[0].Property);
            Assert.Equal(OrderDirection.Ascending, criteria.OrderBy[0].Direction);
        }

        #endregion
    }
}