#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation
{
    public class EkExpressionQueryModelVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly EkExpressionAggregator _aggregator;
        private readonly IEktronIdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public EkExpressionQueryModelVisitor(IEktronIdProvider idProvider)
        {
            _aggregator = new EkExpressionAggregator();
            _idProvider = idProvider;
        }

        #endregion

        #region Public Methods

        public static Ek.Expression Translate(QueryModel queryModel, IEktronIdProvider idProvider)
        {
            var visitor = new EkExpressionQueryModelVisitor(idProvider);
            visitor.VisitQueryModel(queryModel);

            return visitor._aggregator.GetExpression();
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            var smartFormAttr = fromClause.ItemType.GetCustomAttribute<SmartFormAttribute>();
            var contentTypeAttr = fromClause.ItemType.GetCustomAttribute<ContentTypeAttribute>();
            var folderPathAttr = fromClause.ItemType.GetCustomAttribute<FolderPathAttribute>();

            if (smartFormAttr != null)
            {
                var id = _idProvider.GetSmartFormId(smartFormAttr.SmartFormName);
                _aggregator.Add(SearchContentProperty.XmlConfigId == id);
            }
            if (folderPathAttr != null)
            {
                _aggregator.Add(SearchContentProperty.FolderPath == folderPathAttr.FolderPath);
            }
            if (contentTypeAttr != null)
            {
                var mainId = _idProvider.GetContentTypeId(contentTypeAttr.ContentTypeName);
                var subId = string.IsNullOrEmpty(contentTypeAttr.ContentSubTypeName)
                                ? -1
                                : _idProvider.GetContentTypeId(contentTypeAttr.ContentSubTypeName);
                _aggregator.Add(SearchContentProperty.ContentType == mainId);
                if (subId > 0)
                {
                    _aggregator.Add(SearchContentProperty.ContentSubType == subId);
                }
            }

            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            queryModel.MainFromClause.Accept(this, queryModel);
            queryModel.SelectClause.Accept(this, queryModel);
            VisitBodyClauses(queryModel.BodyClauses, queryModel);
        }

        public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
        {
            _aggregator.Add(QueryBuildingVisitor.Build(whereClause.Predicate));

            base.VisitWhereClause(whereClause, queryModel, index);
        }

        #endregion
    }
}