#region Usings

using System;
using System.Linq;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryFactory
    {
        #region Public Methods

        public static EktronQueryable<T> Queryable<T>()
        {
            return new EktronQueryable<T>(CreateQueryParser(), CreateExecutor());
        }

        #endregion

        #region Methods

        private static IQueryExecutor CreateExecutor()
        {
            return new EktronQueryExecutor();
        }

        private static IQueryParser CreateQueryParser()
        {
            return QueryParser.CreateDefault();
        }

        #endregion
    }
}