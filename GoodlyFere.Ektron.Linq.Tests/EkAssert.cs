#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Tests.Ektron;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests
{
    public static class EkAssert
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(EkAssert));

        #endregion

        #region Methods

        internal static void Equal(Expression expectedExpression, Expression actualExpression)
        {
            string expectedQuery = CreateQueryString(
                new AdvancedSearchCriteria { ExpressionTree = expectedExpression });
            string actualQuery = CreateQueryString(
                new AdvancedSearchCriteria { ExpressionTree = actualExpression });

            Assert.Equal(expectedQuery, actualQuery);
        }

        internal static void Equal(AdvancedSearchCriteria expectedCriteria, AdvancedSearchCriteria actualCriteria)
        {
            string expectedQuery = CreateQueryString(expectedCriteria);
            string actualQuery = CreateQueryString(actualCriteria);

            Assert.Equal(expectedQuery, actualQuery);
        }

        internal static T IsExpressionType<T>(Expression translation) where T : Expression
        {
            Assert.IsAssignableFrom<T>(translation);
            return (T)translation;
        }

        private static string CreateQueryString(AdvancedSearchCriteria criteria)
        {
            SS2010TransformVisitor sS2010TransformVisitor = new SS2010TransformVisitor(criteria);
            sS2010TransformVisitor.Transform(criteria.ExpressionTree);
            Expression tree = sS2010TransformVisitor.Tree;
            if (criteria.OrderBy == null || criteria.OrderBy.Count == 0)
            {
                List<OrderData> orderDatas = new List<OrderData>();
                orderDatas.Add(new OrderData(SearchContentProperty.Rank, OrderDirection.Descending));
                criteria.OrderBy = orderDatas;
            }
            SelectExpression selectExpression = SelectExpression
                .Select(criteria.ReturnProperties)
                .From()
                .Where(tree)
                .OrderBy(criteria.OrderBy);
            SS2010ExpressionVisitor sS2010ExpressionVisitor = new SS2010ExpressionVisitor();
            sS2010ExpressionVisitor.Visit(selectExpression);

            string queryString = sS2010ExpressionVisitor.ToString();
            Log.Info(queryString);
            return queryString;
        }

        #endregion
    }
}