#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Aggregators;
using GoodlyFere.Ektron.Linq.Generation.ExpressionTreeVisitors;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.Expressions;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public class SearchQueryModelVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly CriteriaExpressionTreeAggregator _exprTreeAggregator;
        private readonly IEktronIdProvider _idProvider;
        private readonly OrderByAggregator _orderByAggregator;

        #endregion

        #region Constructors and Destructors

        public SearchQueryModelVisitor(IEktronIdProvider idProvider)
        {
            _exprTreeAggregator = new CriteriaExpressionTreeAggregator();
            _orderByAggregator = new OrderByAggregator();
            _idProvider = idProvider;
        }

        #endregion

        #region Properties

        private Ek.Expression ExpressionTree
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
            var visitor = new SearchQueryModelVisitor(idProvider);
            visitor.VisitQueryModel(queryModel);

            AdvancedSearchCriteria criteria = new AdvancedSearchCriteria
                {
                    ExpressionTree = visitor.ExpressionTree,
                    OrderBy = visitor.OrderByData
                };

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
            Ek.PropertyExpression propExpr = QueryBuildingVisitor.Build(ordering.Expression) as Ek.PropertyExpression;
            OrderDirection direction = ordering.OrderingDirection == OrderingDirection.Asc
                                           ? OrderDirection.Ascending
                                           : OrderDirection.Descending;

            _orderByAggregator.Add(new OrderData(propExpr, direction));

            base.VisitOrdering(ordering, queryModel, orderByClause, index);
        }
        
        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            var subQueryVisitor = new SubQueryExpressionVisitor();
            whereClause.TransformExpressions(subQueryVisitor.VisitExpression);

            _exprTreeAggregator.Add(QueryBuildingVisitor.Build(whereClause.Predicate));

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}