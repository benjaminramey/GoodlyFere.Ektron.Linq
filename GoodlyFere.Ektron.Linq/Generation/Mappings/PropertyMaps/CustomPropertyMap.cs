#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps
{
    internal class CustomPropertyMap : PropertyMapBase
    {
        #region Constructors and Destructors

        public CustomPropertyMap()
        {
            Add(typeof(BooleanPropertyExpression), SearchCustomProperty.GetBooleanProperty);
            Add(typeof(DatePropertyExpression), SearchCustomProperty.GetDateProperty);
            Add(typeof(DecimalPropertyExpression), SearchCustomProperty.GetDecimalProperty);
            Add(typeof(IntegerPropertyExpression), SearchCustomProperty.GetIntegerProperty);
            Add(typeof(StringPropertyExpression), SearchCustomProperty.GetStringProperty);
        }

        #endregion
    }
}