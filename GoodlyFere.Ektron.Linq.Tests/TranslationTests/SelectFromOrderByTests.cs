#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectFromOrderByTests.cs">
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
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.TranslationTests
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
            var criteria = CriteriaGenerator.Generate(model, _idProvider);

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
            var criteria = CriteriaGenerator.Generate(model, _idProvider);

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
            var criteria = CriteriaGenerator.Generate(model, _idProvider);

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
            var criteria = CriteriaGenerator.Generate(model, _idProvider);

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