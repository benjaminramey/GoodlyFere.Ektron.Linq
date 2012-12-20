#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Interfaces;
using Remotion.Linq;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryExecutor : IQueryExecutor
    {
        #region Constants and Fields

        private readonly IEktronIdProvider _idProvider;

        #endregion

        #region Constructors and Destructors

        public EktronQueryExecutor(IEktronIdProvider idProvider)
        {
            _idProvider = idProvider;
        }

        #endregion

        #region Public Methods

        public AdvancedSearchCriteria BuildCriteria(QueryModel queryModel)
        {
            return SearchQueryModelVisitor.Translate(queryModel, _idProvider);
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            AdvancedSearchCriteria criteria = BuildCriteria(queryModel);
            criteria.PagingInfo.RecordsPerPage = 10000;

            return new T[0];
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}