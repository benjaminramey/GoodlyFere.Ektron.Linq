#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TranslationVisitor.cs">
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
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Translation.Aggregators;
using GoodlyFere.Ektron.Linq.Generation.Translation.ExpressionVisitors;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using EktronExpression = Ektron.Cms.Search.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.ModelVisitors
{
    public class TranslationVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly CriteriaExpressionTreeAggregator _exprTreeAggregator;
        private readonly IEktronIdProvider _idProvider;
        private readonly OrderByAggregator _orderByAggregator;
        private readonly List<PropertyExpression> _returnProperties;

        #endregion

        #region Constructors and Destructors

        public TranslationVisitor(IEktronIdProvider idProvider)
        {
            _exprTreeAggregator = new CriteriaExpressionTreeAggregator();
            _orderByAggregator = new OrderByAggregator();
            _idProvider = idProvider;
            _returnProperties = new List<PropertyExpression>();
        }

        #endregion

        #region Properties

        private EktronExpression ExpressionTree
        {
            get
            {
                return _exprTreeAggregator.GetExpression();
            }
        }

        private List<OrderData> OrderByData
        {
            get
            {
                return _orderByAggregator.GetOrderByData();
            }
        }

        #endregion

        #region Public Methods

        public static AdvancedSearchCriteria Translate(QueryModel queryModel, IEktronIdProvider idProvider)
        {
            var visitor = new TranslationVisitor(idProvider);
            visitor.VisitQueryModel(queryModel);
            
            AdvancedSearchCriteria criteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = visitor.ExpressionTree,
                    OrderBy = visitor.OrderByData,
                    ReturnProperties = new HashSet<PropertyExpression>(visitor._returnProperties)
                };

            if (criteria.ExpressionTree == null)
            {
                criteria.ExpressionTree = SearchType.IsContent();
            }

            return criteria;
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            var smartFormAttr = fromClause.ItemType.GetCustomAttribute<SmartFormAttribute>();
            var contentTypeAttr = fromClause.ItemType.GetCustomAttribute<ContentTypeAttribute>();
            var folderPathAttr = fromClause.ItemType.GetCustomAttribute<FolderPathAttribute>();

            if (smartFormAttr != null)
            {
                var id = _idProvider.GetSmartFormId(smartFormAttr.SmartFormName);
                _exprTreeAggregator.Add(SearchContentProperty.XmlConfigId == id);
            }
            if (folderPathAttr != null)
            {
                _exprTreeAggregator.Add(SearchContentProperty.FolderPath == folderPathAttr.FolderPath);
            }
            if (contentTypeAttr != null)
            {
                var mainId = _idProvider.GetContentTypeId(contentTypeAttr.ContentTypeName);
                var subId = string.IsNullOrEmpty(contentTypeAttr.ContentSubTypeName)
                                ? -1
                                : _idProvider.GetContentTypeId(contentTypeAttr.ContentSubTypeName);
                _exprTreeAggregator.Add(SearchContentProperty.ContentType == mainId);
                if (subId > 0)
                {
                    _exprTreeAggregator.Add(SearchContentProperty.ContentSubType == subId);
                }
            }

            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitOrdering(
            Ordering ordering, QueryModel queryModel, OrderByClause orderByClause, int index)
        {
            QueryBuildResult result = QueryBuildingVisitor.Build(ordering.Expression);
            PropertyExpression propExpr = result.Expression as PropertyExpression;
            OrderDirection direction = ordering.OrderingDirection == OrderingDirection.Asc
                                           ? OrderDirection.Ascending
                                           : OrderDirection.Descending;

            _orderByAggregator.Add(new OrderData(propExpr, direction));
            _returnProperties.AddRange(result.PropertiesUsed);

            base.VisitOrdering(ordering, queryModel, orderByClause, index);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            QueryBuildResult result = QueryBuildingVisitor.Build(whereClause.Predicate);
            _exprTreeAggregator.Add(result.Expression);
            _returnProperties.AddRange(result.PropertiesUsed);

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}