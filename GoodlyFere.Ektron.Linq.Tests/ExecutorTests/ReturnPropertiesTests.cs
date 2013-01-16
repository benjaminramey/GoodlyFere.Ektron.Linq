#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using NSubstitute;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.ExecutorTests
{
    public class ReturnPropertiesTests
    {
        #region Constants and Fields

        private readonly TestableExecutor _executor;
        private readonly IdProvider _idProvider;
        private readonly ISearchManager _searchManager;

        #endregion

        #region Constructors and Destructors

        public ReturnPropertiesTests()
        {
            _idProvider = new IdProvider();
            _searchManager = Substitute.For<ISearchManager>();
            _executor = new TestableExecutor(_idProvider, _searchManager);
        }

        #endregion

        #region Public Methods

        [Fact]
        public void IncludesAllEktronPropertyAttributes()
        {
            var query = from w in EktronQueryFactory.Queryable<Widget>()
                        where w.Id == 1
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = _executor.BuildCriteria<Widget>(model, 1000);

            Assert.Equal(1, criteria.ReturnProperties.Count);
            Assert.Equal("ContentId".ToLower(), criteria.ReturnProperties.First().Name);
        }

        [Fact]
        public void NoAttributesOnProperties()
        {
            var query = from w in EktronQueryFactory.Queryable<NoAttributesWidget>()
                        where w.ContentId == 1
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = _executor.BuildCriteria<NoAttributesWidget>(model, 1000);

            Assert.True(criteria.ReturnProperties.Count > 0, "0 return properties found");
            Assert.Equal("ContentId", criteria.ReturnProperties.First().Name);
        }

        #endregion
    }
}