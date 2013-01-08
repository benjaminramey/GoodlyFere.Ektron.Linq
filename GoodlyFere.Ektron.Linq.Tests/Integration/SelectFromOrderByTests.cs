#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
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
            var criteria = new EktronQueryExecutor(_idProvider, ObjectFactory.GetSearchManager()).BuildCriteria(model);

            var actualCriteria = criteria;
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = SearchContentProperty.XmlConfigId == expectedId,
                    OrderBy = new List<OrderData> { new OrderData(SearchContentProperty.Id, OrderDirection.Ascending) }
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        [Fact]
        public void SelectFromOrderByDescending()
        {
            var expectedId = _idProvider.GetSmartFormId(SmartFormWidget.SmartFormName);

            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>(_idProvider)
                        orderby w.Id descending
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = new EktronQueryExecutor(_idProvider, ObjectFactory.GetSearchManager()).BuildCriteria(model);

            var actualCriteria = criteria;
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = SearchContentProperty.XmlConfigId == expectedId,
                    OrderBy = new List<OrderData> { new OrderData(SearchContentProperty.Id, OrderDirection.Descending) }
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        [Fact]
        public void SelectFromOrderBy_Multiple()
        {
            var expectedId = _idProvider.GetSmartFormId(SmartFormWidget.SmartFormName);

            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>(_idProvider)
                        orderby w.Id, w.Name
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = new EktronQueryExecutor(_idProvider, ObjectFactory.GetSearchManager()).BuildCriteria(model);

            var actualCriteria = criteria;
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = SearchContentProperty.XmlConfigId == expectedId,
                    OrderBy = new List<OrderData>
                        {
                            new OrderData(SearchContentProperty.Id, OrderDirection.Ascending),
                            new OrderData(new StringPropertyExpression("Name"), OrderDirection.Ascending)
                        }
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        [Fact]
        public void SelectFromOrderBy_MultipleWithMixedOrdering()
        {
            var expectedId = _idProvider.GetSmartFormId(SmartFormWidget.SmartFormName);

            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>(_idProvider)
                        orderby w.Id, w.Name descending
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = new EktronQueryExecutor(_idProvider, ObjectFactory.GetSearchManager()).BuildCriteria(model);

            var actualCriteria = criteria;
            var expectedCriteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = SearchContentProperty.XmlConfigId == expectedId,
                    OrderBy = new List<OrderData>
                        {
                            new OrderData(SearchContentProperty.Id, OrderDirection.Ascending),
                            new OrderData(new StringPropertyExpression("Name"), OrderDirection.Descending)
                        }
                };
            EkAssert.Equal(expectedCriteria, actualCriteria);
        }

        #endregion
    }
}