#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps
{
    internal class MetadataPropertyMap : PropertyMapBase
    {
        #region Constructors and Destructors

        public MetadataPropertyMap()
        {
            Add(typeof(BooleanPropertyExpression), SearchMetadataProperty.GetBooleanProperty);
            Add(typeof(DatePropertyExpression), SearchMetadataProperty.GetDateProperty);
            Add(typeof(DecimalPropertyExpression), SearchMetadataProperty.GetDecimalProperty);
            Add(typeof(IntegerPropertyExpression), SearchMetadataProperty.GetIntegerProperty);
            Add(typeof(StringPropertyExpression), SearchMetadataProperty.GetStringProperty);
        }

        #endregion
    }
}