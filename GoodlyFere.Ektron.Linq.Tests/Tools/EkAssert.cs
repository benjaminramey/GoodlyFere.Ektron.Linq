#region Usings

using System;
using System.Linq;
using Common.Logging;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Tests.Tools.Extensions;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Tools
{
    public static class EkAssert
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(EkAssert));

        #endregion

        #region Methods

        internal static void Equal(Expression expectedExpression, Expression actualExpression)
        {
            string expectedQuery = expectedExpression.ToTestString();
            string actualQuery = actualExpression.ToTestString();

            Log.InfoFormat("Expected: {0}", expectedQuery);
            Log.InfoFormat("Actual: {0}", actualQuery);

            Assert.Equal(expectedQuery, actualQuery);
        }

        internal static void Equal(AdvancedSearchCriteria expectedCriteria, AdvancedSearchCriteria actualCriteria)
        {
            string expectedQuery = expectedCriteria.ExpressionTree.ToTestString();
            string actualQuery = actualCriteria.ExpressionTree.ToTestString();

            Log.InfoFormat("Expected: {0}", expectedQuery);
            Log.InfoFormat("Actual: {0}", actualQuery);

            Assert.Equal(expectedQuery, actualQuery);
        }

        internal static T IsExpressionType<T>(Expression translation) where T : Expression
        {
            Assert.IsAssignableFrom<T>(translation);
            return (T)translation;
        }

        #endregion
    }
}