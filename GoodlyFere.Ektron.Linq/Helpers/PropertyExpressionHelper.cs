#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Model.Attributes;

#endregion

namespace GoodlyFere.Ektron.Linq.Helpers
{
    public static class PropertyExpressionHelper
    {
        #region Public Methods

        public static PropertyExpression GetCustomPropertyExpression(string propertyName, Type ekPropertyType)
        {
            PropertyExpression propExr;

            if (ekPropertyType == typeof(BooleanPropertyExpression))
            {
                propExr = SearchCustomProperty.GetBooleanProperty(propertyName);
            }
            else if (ekPropertyType == typeof(DatePropertyExpression))
            {
                propExr = SearchCustomProperty.GetDateProperty(propertyName);
            }
            else if (ekPropertyType == typeof(DecimalPropertyExpression))
            {
                propExr = SearchCustomProperty.GetDecimalProperty(propertyName);
            }
            else if (ekPropertyType == typeof(IntegerPropertyExpression))
            {
                propExr = SearchCustomProperty.GetIntegerProperty(propertyName);
            }
            else
            {
                propExr = SearchCustomProperty.GetStringProperty(propertyName);
            }

            return propExr;
        }

        public static PropertyExpression GetMetadataPropertyExpression(string propertyName, Type ekPropertyType)
        {
            PropertyExpression propExr;

            if (ekPropertyType == typeof(BooleanPropertyExpression))
            {
                propExr = SearchMetadataProperty.GetBooleanProperty(propertyName);
            }
            else if (ekPropertyType == typeof(DatePropertyExpression))
            {
                propExr = SearchMetadataProperty.GetDateProperty(propertyName);
            }
            else if (ekPropertyType == typeof(DecimalPropertyExpression))
            {
                propExr = SearchMetadataProperty.GetDecimalProperty(propertyName);
            }
            else if (ekPropertyType == typeof(IntegerPropertyExpression))
            {
                propExr = SearchMetadataProperty.GetIntegerProperty(propertyName);
            }
            else
            {
                propExr = SearchMetadataProperty.GetStringProperty(propertyName);
            }

            return propExr;
        }

        public static PropertyExpression GetPropertyExpression(EktronPropertyAttribute attr)
        {
            PropertyExpression property;

            if (attr.IsCustomProperty)
            {
                property = GetCustomPropertyExpression(attr.EktronPropertyName, attr.EktronExpressionType);
            }
            else if (attr.IsSmartFormProperty)
            {
                property = GetSmartFormPropertyExpression(attr.EktronPropertyName, attr.EktronExpressionType);
            }
            else if (attr.IsMetadataProperty)
            {
                property = GetMetadataPropertyExpression(attr.EktronPropertyName, attr.EktronExpressionType);
            }
            else
            {
                property = GetRegularPropertyExpression(attr.EktronPropertyName, attr.EktronExpressionType);
            }

            return property;
        }

        public static HashSet<PropertyExpression> GetPropertyExpressionsForType(Type domainObjectType)
        {
            HashSet<PropertyExpression> properties = new HashSet<PropertyExpression>();

            TraverseEktronProperties(
                attr =>
                    {
                        PropertyExpression propExpr = GetPropertyExpression(attr);
                        if (propExpr != null)
                        {
                            properties.Add(propExpr);
                        }
                    },
                domainObjectType);

            return properties;
        }

        public static string GetPropertyName(EktronPropertyAttribute attr)
        {
            PropertyExpression propExpr = GetPropertyExpression(attr);
            return propExpr == null ? string.Empty : propExpr.Name;
        }

        public static PropertyExpression GetRegularPropertyExpression(string propertyName, Type ekPropertyType)
        {
            PropertyExpression propExr;

            if (ekPropertyType == typeof(BooleanPropertyExpression))
            {
                propExr = new BooleanPropertyExpression(propertyName);
            }
            else if (ekPropertyType == typeof(DatePropertyExpression))
            {
                propExr = new DatePropertyExpression(propertyName);
            }
            else if (ekPropertyType == typeof(DecimalPropertyExpression))
            {
                propExr = new DecimalPropertyExpression(propertyName);
            }
            else if (ekPropertyType == typeof(IntegerPropertyExpression))
            {
                propExr = new IntegerPropertyExpression(propertyName);
            }
            else
            {
                propExr = new StringPropertyExpression(propertyName);
            }

            return propExr;
        }

        public static PropertyExpression GetSmartFormPropertyExpression(string propertyName, Type ekPropertyType)
        {
            PropertyExpression propExr;

            if (ekPropertyType == typeof(BooleanPropertyExpression))
            {
                propExr = SearchSmartFormProperty.GetBooleanProperty(propertyName);
            }
            else if (ekPropertyType == typeof(DatePropertyExpression))
            {
                propExr = SearchSmartFormProperty.GetDateProperty(propertyName);
            }
            else if (ekPropertyType == typeof(DecimalPropertyExpression))
            {
                propExr = SearchSmartFormProperty.GetDecimalProperty(propertyName);
            }
            else if (ekPropertyType == typeof(IntegerPropertyExpression))
            {
                propExr = SearchSmartFormProperty.GetIntegerProperty(propertyName);
            }
            else
            {
                propExr = SearchSmartFormProperty.GetStringProperty(propertyName);
            }

            return propExr;
        }

        #endregion

        #region Methods

        private static void TraverseEktronProperties(Action<EktronPropertyAttribute> callback, Type domainObjectType)
        {
            foreach (var propertyInfo in domainObjectType.GetProperties())
            {
                var attr = propertyInfo.GetCustomAttribute<EktronPropertyAttribute>();
                if (attr == null)
                {
                    continue;
                }

                callback(attr);
            }
        }

        #endregion
    }
}