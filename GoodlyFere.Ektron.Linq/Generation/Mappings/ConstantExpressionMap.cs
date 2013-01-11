#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantExpressionMap.cs">
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
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal class ConstantExpressionMap : Dictionary<Type, Func<object, Expression>>
    {
        #region Constructors and Destructors

        public ConstantExpressionMap()
        {
            Add(typeof(bool), v => new BooleanValueExpression((bool)v));
            Add(typeof(DateTime), v => new DateValueExpression((DateTime)v));
            Add(typeof(double), v => new DecimalValueExpression((double)v));
            Add(typeof(long), v => new IntegerValueExpression((long)v));
            Add(typeof(string), v => new StringValueExpression((string)v));
            Add(typeof(int), v => new IntegerValueExpression(Convert.ToInt64(v)));
        }

        #endregion
    }
}