#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    public class EktronQueryable<T> : QueryableBase<T>
    {
        #region Constructors and Destructors

        public EktronQueryable(IQueryParser queryParser, IQueryExecutor executor)
            : base(new DefaultQueryProvider(typeof(EktronQueryable<>), queryParser, executor))
        {
        }

        public EktronQueryable(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        #endregion
    }
}