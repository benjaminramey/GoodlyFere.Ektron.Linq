#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal class ConstantExpressionMap : Dictionary<Type, Func<object, Expression>>
    {
        #region Constructors and Destructors

        public ConstantExpressionMap()
        {
            Add(typeof(bool), v => new BooleanValueExpression((bool)v));
            Add(typeof(DateTime), v => new DateValueExpression((DateTime)v));
            Add(typeof(double), v => new DecimalValueExpression((double)v));
            Add(typeof(long), v => new IntegerValueExpression((long)v));
            Add(typeof(string), v => new StringValueExpression((string)v));
            Add(typeof(int), v => new IntegerValueExpression(Convert.ToInt64(v)));
        }

        #endregion
    }
}