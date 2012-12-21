#region Usings

using System;
using System.Linq;
using Ektron.Cms;
using GoodlyFere.Ektron.Linq.Interfaces;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryFactory
    {
        #region Public Methods

        public static EktronQueryable<T> Queryable<T>(IEktronIdProvider idProvider)
        {
            return new EktronQueryable<T>(CreateQueryParser(), CreateExecutor(idProvider));
        }

        #endregion

        #region Methods

        private static IQueryExecutor CreateExecutor(IEktronIdProvider idProvider)
        {
            return new EktronQueryExecutor(idProvider, ObjectFactory.GetSearchManager());
        }

        private static IQueryParser CreateQueryParser()
        {
            return QueryParser.CreateDefault();
        }

        #endregion
    }
}