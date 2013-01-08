#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps
{
    internal class SmartFormPropertyMap : PropertyMapBase
    {
        #region Constructors and Destructors

        public SmartFormPropertyMap()
        {
            Add(typeof(BooleanPropertyExpression), SearchSmartFormProperty.GetBooleanProperty);
            Add(typeof(DatePropertyExpression), SearchSmartFormProperty.GetDateProperty);
            Add(typeof(DecimalPropertyExpression), SearchSmartFormProperty.GetDecimalProperty);
            Add(typeof(IntegerPropertyExpression), SearchSmartFormProperty.GetIntegerProperty);
            Add(typeof(StringPropertyExpression), SearchSmartFormProperty.GetStringProperty);
        }

        #endregion
    }
}