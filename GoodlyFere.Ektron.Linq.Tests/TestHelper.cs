#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests
{
    internal class TestHelper
    {
        #region Methods

        internal static QueryModel GetQueryModel<T>(IQueryable<T> query)
        {
            return QueryParser.CreateDefault().GetParsedQuery(query.Expression);
        }

        internal static Expression GetTranslation<T>(IQueryable<T> query)
        {
            var queryModel = GetQueryModel(query);
            return SearchQueryModelVisitor.Translate(queryModel, new IdProvider()).ExpressionTree;
        }

        #endregion
    }
}