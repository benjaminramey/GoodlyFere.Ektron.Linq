﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyMapBase.cs">
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

using System.Collections.Generic;
using System.Linq;
using System;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.Maps.PropertyMaps
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