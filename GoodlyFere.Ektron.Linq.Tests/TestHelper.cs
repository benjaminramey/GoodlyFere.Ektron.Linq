#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests
{
    internal class TestHelper
    {
        #region Methods

        internal static Expression GetTranslation<T>(IQueryable<T> query)
        {
            var queryModel = QueryParser.CreateDefault().GetParsedQuery(query.Expression);
            return EkExpressionQueryModelVisitor.Translate(queryModel, new IdProvider());
        }

        #endregion
    }
}