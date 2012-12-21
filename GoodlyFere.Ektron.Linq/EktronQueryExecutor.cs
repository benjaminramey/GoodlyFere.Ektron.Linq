#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Ektron.Cms.Search;
using GoodlyFere.Ektron.Linq.Generation;
using GoodlyFere.Ektron.Linq.Helpers;
using GoodlyFere.Ektron.Linq.Interfaces;
using GoodlyFere.Ektron.Linq.Search;
using Remotion.Linq;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryExecutor : IQueryExecutor
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger<EktronQueryExecutor>();

        private readonly IEktronIdProvider _idProvider;
        private readonly EktronSearcher _searcher;

        #endregion

        #region Constructors and Destructors

        public EktronQueryExecutor(IEktronIdProvider idProvider, ISearchManager searchManager)
        {
            _idProvider = idProvider;
            _searcher = new EktronSearcher(searchManager);
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
            criteria.Permission = Permission.CreateAdministratorPermission();
            criteria.ReturnProperties = PropertyExpressionHelper.GetPropertyExpressionsForType(typeof(T));

            var results = _searcher.DoSearch(criteria);
            var converter = new ResultsConverter<T>();
            return converter.GetMany(results, criteria);
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            AdvancedSearchCriteria criteria = BuildCriteria(queryModel);
            criteria.PagingInfo.RecordsPerPage = 1;
            criteria.Permission = Permission.CreateAdministratorPermission();
            criteria.ReturnProperties = PropertyExpressionHelper.GetPropertyExpressionsForType(typeof(T));

            var results = _searcher.DoSearch(criteria);
            var converter = new ResultsConverter<T>();

            T result = converter.GetSingle(results, criteria);
            if (result == null && !returnDefaultWhenEmpty)
            {
                throw new InvalidOperationException("The input sequence is empty.");
            }

            return result;
        }

        #endregion
    }
}