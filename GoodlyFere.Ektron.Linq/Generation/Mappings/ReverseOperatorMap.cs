#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal class ReverseOperatorMap : Dictionary<ExpressionType, ExpressionType>
    {
        #region Constructors and Destructors

        public ReverseOperatorMap()
        {
            Add(ExpressionType.GreaterThan, ExpressionType.LessThan);
            Add(ExpressionType.LessThan, ExpressionType.GreaterThan);
            Add(ExpressionType.GreaterThanOrEqual, ExpressionType.LessThanOrEqual);
            Add(ExpressionType.LessThanOrEqual, ExpressionType.GreaterThanOrEqual);
        }

        #endregion
    }
}