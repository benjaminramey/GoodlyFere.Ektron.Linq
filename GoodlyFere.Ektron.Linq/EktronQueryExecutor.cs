#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Remotion.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryExecutor : IQueryExecutor
    {
        #region Public Methods

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {
            throw new NotImplementedException();
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