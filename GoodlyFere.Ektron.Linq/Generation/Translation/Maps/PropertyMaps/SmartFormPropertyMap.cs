#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmartFormPropertyMap.cs">
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
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.Maps.PropertyMaps
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