#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnaryExpressionMap.cs">
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
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Generation.Translation.Handlers.Expressions;
using EktronExpression = Ektron.Cms.Search.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.Maps
{
    internal delegate EktronExpression UnaryExpressionFactoryMethod(
        UnaryExpression unaryExpression, EktronExpression operand);

    internal class UnaryExpressionMap : Dictionary<ExpressionType, UnaryExpressionFactoryMethod>
    {
        #region Constructors and Destructors

        public UnaryExpressionMap()
        {
            Add(ExpressionType.Not, (ue, operand) => new NotExpression(operand));
            Add(ExpressionType.Convert, UnaryExpressionHandlers.HandleConvert);
        }

        #endregion
    }
}