#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps;
using GoodlyFere.Ektron.Linq.Model.Attributes;

#endregion

namespace GoodlyFere.Ektron.Linq.Helpers
{
    public static class PropertyExpressionHelper
    {
        #region Public Methods

        public static PropertyExpression GetPropertyExpression(EktronPropertyAttribute attr)
        {
            PropertyMapBase map;
            if (attr.IsCustomProperty)
            {
                map = new CustomPropertyMap();
            }
            else if (attr.IsSmartFormProperty)
            {
                map = new SmartFormPropertyMap();
            }
            else if (attr.IsMetadataProperty)
            {
                map = new MetadataPropertyMap();
            }
            else
            {
                map = new RegularPropertyMap();
            }

            Func<string, PropertyExpression> factoryMethod = map.FirstOrDefault(attr.EktronExpressionType);
            return factoryMethod.Invoke(attr.EktronPropertyName);
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