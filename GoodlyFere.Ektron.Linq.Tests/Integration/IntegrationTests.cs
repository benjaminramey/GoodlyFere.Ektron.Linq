#region Usings

using System;
using System.Configuration;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.EktronModel.Attributes;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Tests.Model;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.Structure;
using Xunit;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Integration
{
    internal class IdProvider : IEktronIdProvider
    {
        public long GetContentTypeId(string name)
        {
            string key = string.Concat(name, "ContentType");
            string value = ConfigurationManager.AppSettings[key];
            return long.Parse(value);
        }

        public long GetSmartFormId(string name)
        {
            string key = string.Concat(name, "SmartForm");
            string value = ConfigurationManager.AppSettings[key];
            return long.Parse(value);
        }
    }

    [SmartForm("Widget")]
    internal class SmartFormWidget : Widget
    {

    }

    public class IntegrationTests
    {
        // select-from produces null expression

        #region Public Methods

        [Fact]
        public void SelectFrom_PlainObject_TranslatesToNull()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        select w;

            var translation = GetTranslation(query);
            Assert.Null(translation);
        }

        [Fact]
        public void SelectFrom_SmartFormAttrObj_TranslatesToXmlConfigIdExpression()
        {
            var expectedSmartFormId = new IdProvider().GetSmartFormId("Widget");
            var query = from w in EktronQueryFactory.Queryable<SmartFormWidget>()
                        select w;

            var actualTranslation = GetTranslation(query);
            var expectedTranslation = SearchContentProperty.XmlConfigId == expectedSmartFormId;
            EkAssert.Equal(expectedTranslation, actualTranslation);
        }


        #endregion

        #region Methods

        private Expression GetTranslation<T>(IQueryable<T> query)
        {
            var queryModel = QueryParser.CreateDefault().GetParsedQuery(query.Expression);
            return EkExpressionQueryModelVisitor.Translate(queryModel);
        }

        #endregion
    }

    public class EkExpressionQueryModelVisitor : QueryModelVisitorBase
    {
        #region Constants and Fields

        private readonly IEktronIdProvider _idProvider;
        private Expression _translation;

        #endregion
        public EkExpressionQueryModelVisitor(IEktronIdProvider idProvider)
        {
            _idProvider = idProvider;
        }

        #region Public Methods

        public static Expression Translate(QueryModel queryModel)
        {
            var visitor = new EkExpressionQueryModelVisitor(new IdProvider());
            visitor.VisitQueryModel(queryModel);
            
            return visitor._translation;
        }

        public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
        {
            var attr = fromClause.ItemType.GetCustomAttribute<SmartFormAttribute>();
            var id = _idProvider.GetSmartFormId(attr.SmartFormName);

            _translation = SearchContentProperty.XmlConfigId == id;

            base.VisitMainFromClause(fromClause, queryModel);
        }

        public override void VisitQueryModel(QueryModel queryModel)
        {
            queryModel.MainFromClause.Accept(this, queryModel);
            queryModel.SelectClause.Accept(this, queryModel);
        }

        #endregion
    }
}