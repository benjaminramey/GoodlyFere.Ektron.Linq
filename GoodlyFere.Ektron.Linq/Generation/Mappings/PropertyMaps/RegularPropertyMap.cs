#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps
{
    internal class RegularPropertyMap : PropertyMapBase
    {
        #region Constructors and Destructors

        public RegularPropertyMap()
        {
            Add(typeof(BooleanPropertyExpression), s => new BooleanPropertyExpression(s));
            Add(typeof(DatePropertyExpression), s => new DatePropertyExpression(s));
            Add(typeof(DecimalPropertyExpression), s => new DecimalPropertyExpression(s));
            Add(typeof(IntegerPropertyExpression), s => new IntegerPropertyExpression(s));
            Add(typeof(StringPropertyExpression), s => new StringPropertyExpression(s));
        }

        #endregion
    }
}