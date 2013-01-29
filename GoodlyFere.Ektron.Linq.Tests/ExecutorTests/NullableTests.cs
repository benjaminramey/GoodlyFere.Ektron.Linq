#region Usings

using System;
using System.Linq;
using System.ServiceModel;
using Ektron.Cms;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Execution.Search;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Tests.Model;
using GoodlyFere.Ektron.Linq.Tests.TestImplementations;
using GoodlyFere.Ektron.Linq.Tests.Tools;
using Xunit;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.ExecutorTests
{
    public class NullableTests
    {
        #region Constants and Fields

        private readonly IdProvider _idProvider;
        private readonly ISearchManager _searchManager;
        private EktronSearcher _searcher;

        #endregion

        #region Constructors and Destructors

        public NullableTests()
        {
            _idProvider = new IdProvider();
            _searchManager = ObjectFactory.GetSearchManager();
            _searcher = new EktronSearcher(_searchManager);
        }

        #endregion

        #region Public Methods

        [Fact]
        public void IsNotNull()
        {
            var query = from w in EktronQueryFactory.Queryable<NullableWidget>()
                        where w.NullableDecimal.HasValue
                        select w;

            var model = TestHelper.GetQueryModel(query);
            var criteria = CriteriaGenerator.Generate(model, _idProvider);
            
            Assert.Throws<FaultException<ExceptionDetail>>(() => _searcher.DoSearch(criteria));
        }

        #endregion
    }
}