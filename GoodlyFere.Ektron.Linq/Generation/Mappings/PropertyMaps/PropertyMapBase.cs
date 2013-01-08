#region Usings

using System.Collections.Generic;
using System.Linq;
using System;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps
{
    internal class PropertyMapBase : Dictionary<Type, Func<string, PropertyExpression>>,
                                     IDefaultingMap<Type, Func<string, PropertyExpression>>
    {
        #region Public Properties

        public virtual Func<string, PropertyExpression> Default
        {
            get
            {
                return this[typeof(StringPropertyExpression)];
            }
        }

        #endregion

        #region Public Methods

        public virtual Func<string, PropertyExpression> FirstOrDefault(Type propertyExpressionType)
        {
            if (propertyExpressionType != null && ContainsKey(propertyExpressionType))
            {
                return this[propertyExpressionType];
            }

            return Default;
        }

        #endregion
    }
}