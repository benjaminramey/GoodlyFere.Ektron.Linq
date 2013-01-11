#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryExpressionMap.cs">
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
using System.Linq.Expressions;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal delegate Ek.Expression BinaryExpressionFactoryMethod(Ek.Expression propExpr, Ek.Expression valueExpr);

    internal class BinaryExpressionMap : Dictionary<ExpressionType, BinaryExpressionFactoryMethod>
    {
        #region Constructors and Destructors

        public BinaryExpressionMap()
        {
            Add(ExpressionType.Equal, (pe, ve) => new Ek.EqualsExpression(pe, ve));
            Add(ExpressionType.AndAlso, (pe, ve) => new Ek.AndExpression(pe, ve));
            Add(ExpressionType.OrElse, (pe, ve) => new Ek.OrExpression(pe, ve));
            Add(ExpressionType.GreaterThan, (pe, ve) => new Ek.GreaterThanExpression(pe, ve));
            Add(ExpressionType.LessThan, (pe, ve) => new Ek.LessThanExpression(pe, ve));
            Add(ExpressionType.GreaterThanOrEqual, (pe, ve) => new Ek.GreaterThanOrEqualsExpression(pe, ve));
            Add(ExpressionType.LessThanOrEqual, (pe, ve) => new Ek.LessThanOrEqualsExpression(pe, ve));
            Add(ExpressionType.NotEqual, (pe, ve) => new Ek.NotEqualsExpression(pe, ve));
        }

        #endregion
    }
}