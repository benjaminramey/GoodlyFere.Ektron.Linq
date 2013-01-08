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
    public class IntegrationTests
    {
        #region Constants and Fields

        private readonly IdProvider _idProvider;
        private readonly ISearchManager _searchManager;

        #endregion

        #region Constructors and Destructors

        public IntegrationTests()
        {
            _searchManager = ObjectFactory.GetSearchManager();
            _idProvider = new IdProvider();
        }

        #endregion

        #region Public Methods

        [Fact]
        public void Search_ById()
        {
            long expectedId = 100000L;
            var asc = new AdvancedSearchCriteria();
            asc.ExpressionTree = SearchContentProperty.Id > expectedId;
            asc.PagingInfo.RecordsPerPage = 10000;
            asc.ReturnProperties = new HashSet<PropertyExpression> { SearchContentProperty.Id };

            var query = from w in EktronQueryFactory.Queryable<ModelWidget>(_idProvider)
                        where (long)w.Id > expectedId
                        select w;

            var queryResults = query.ToArray();
            var searchResults = _searchManager.Search(asc);

            Console.WriteLine("Query results: {0}", queryResults.Length);
            Console.WriteLine("Search results: {0}", searchResults.Results.Count);
            Assert.Equal(searchResults.Results.Count, queryResults.Length);
        }

        [Fact]
        public void Search_SmartForm_ById()
        {
            object expectedId = 4294967325L;
            var asc = new AdvancedSearchCriteria();
            asc.ExpressionTree = SearchContentProperty.XmlConfigId == 4294967296
                & SearchContentProperty.Id == (long)expectedId;
            asc.PagingInfo.RecordsPerPage = 10000;
            asc.ReturnProperties = new HashSet<PropertyExpression> { SearchContentProperty.Id };

            var query = from w in EktronQueryFactory.Queryable<Practice>(_idProvider)
                        where w.Id == expectedId
                        select w;

            var queryResults = query.ToArray();
            var searchResults = _searchManager.Search(asc);

            Console.WriteLine("Query results: {0}", queryResults.Length);
            Console.WriteLine("Search results: {0}", searchResults.Results.Count);
            Assert.Equal(searchResults.Results.Count, queryResults.Length);
        }

        #endregion
    }
}