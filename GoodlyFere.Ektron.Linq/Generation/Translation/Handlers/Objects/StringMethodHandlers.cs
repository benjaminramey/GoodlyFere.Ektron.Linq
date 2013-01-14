﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringMethodHandlers.cs">
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Ektron.Linq.Generation.Translation.ExpressionVisitors;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.Handlers.Objects
{
    public static class StringMethodHandlers
    {
        #region Public Methods

        public static global::Ektron.Cms.Search.Expressions.Expression HandleStringContains(
            Expression obj, ReadOnlyCollection<Expression> arguments)
        {
            global::Ektron.Cms.Search.Expressions.StringPropertyExpression objectExpr =
                QueryBuildingVisitor.Build(obj) as global::Ektron.Cms.Search.Expressions.StringPropertyExpression;
            global::Ektron.Cms.Search.Expressions.StringValueExpression argExpr =
                QueryBuildingVisitor.Build(arguments[0]) as global::Ektron.Cms.Search.Expressions.StringValueExpression;

            return objectExpr.Contains(argExpr.Value, global::Ektron.Cms.Search.Expressions.WordForms.Inflections);
        }

        #endregion
    }
}