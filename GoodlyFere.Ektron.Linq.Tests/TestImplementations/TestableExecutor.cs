#region Usings

using System.Linq;
using System;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Interfaces;
using Remotion.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.TestImplementations
{
    public class TestableExecutor : EktronQueryExecutor
    {
        #region Constructors and Destructors

        public TestableExecutor(IEktronIdProvider idProvider, ISearchManager searchManager)
            : base(idProvider, searchManager)
        {
        }

        #endregion

        #region Public Methods

        public AdvancedSearchCriteria BuildCriteria<T>(QueryModel model, int recordPerPage)
        {
            return CreateCriteria<T>(model, recordPerPage);
        }

        #endregion
    }
}