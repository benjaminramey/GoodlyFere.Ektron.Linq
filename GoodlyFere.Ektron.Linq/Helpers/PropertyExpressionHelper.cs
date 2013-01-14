#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyExpressionHelper.cs">
// LINQ to Ektron Search, a LINQ interface to the Ektron AdvancedSearchCriteria search engine
// 
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Translation.Maps.PropertyMaps;
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