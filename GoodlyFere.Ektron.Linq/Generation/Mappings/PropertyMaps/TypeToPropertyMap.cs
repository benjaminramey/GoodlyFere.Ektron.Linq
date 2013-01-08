#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings.PropertyMaps
{
    internal class TypeToPropertyMap : Dictionary<Type, Func<PropertyExpression, PropertyExpression>>,
                                       IDefaultingMap<Type, Func<PropertyExpression, PropertyExpression>>
    {
        #region Constructors and Destructors

        public TypeToPropertyMap()
        {
            Add(typeof(long), pe => new IntegerPropertyExpression(pe.Name));
            Add(typeof(string), pe => new StringPropertyExpression(pe.Name));
            Add(typeof(DateTime), pe => new DatePropertyExpression(pe.Name));
            Add(typeof(bool), pe => new BooleanPropertyExpression(pe.Name));
            Add(typeof(double), pe => new DecimalPropertyExpression(pe.Name));
        }

        #endregion

        #region Public Properties

        public Func<PropertyExpression, PropertyExpression> Default
        {
            get
            {
                return this[typeof(string)];
            }
        }

        #endregion

        #region Public Methods

        public Func<PropertyExpression, PropertyExpression> FirstOrDefault(Type key)
        {
            if (key != null && ContainsKey(key))
            {
                return this[key];
            }

            return Default;
        }

        #endregion
    }
}