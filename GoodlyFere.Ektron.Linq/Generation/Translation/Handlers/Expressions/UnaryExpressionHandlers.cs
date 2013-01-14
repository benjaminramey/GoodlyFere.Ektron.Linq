#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnaryExpressionHandlers.cs">
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
using System.Linq.Expressions;
using Ektron.Cms.Search.Expressions;
using GoodlyFere.Ektron.Linq.Exceptions;
using GoodlyFere.Ektron.Linq.Extensions;
using GoodlyFere.Ektron.Linq.Generation.Translation.Maps.PropertyMaps;
using GoodlyFere.Ektron.Linq.Model.Attributes;
using EktronExpression = Ektron.Cms.Search.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Translation.Handlers.Expressions
{
    internal class UnaryExpressionHandlers
    {
        #region Public Methods

        public static EktronExpression HandleConvert(
            UnaryExpression unaryExpression, EktronExpression operand)
        {
            if (unaryExpression.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentOutOfRangeException(
                    "unaryExpression", "unaryExpression must be of NodeType ExpressionType.Convert");
            }

            if (operand is PropertyExpression && unaryExpression.Operand is MemberExpression)
            {
                MemberExpression memberExpr = (MemberExpression)unaryExpression.Operand;
                EktronPropertyAttribute ekProp = memberExpr.Member
                                                           .GetCustomAttribute<EktronPropertyAttribute>();
                TypeToPropertyMap map = new TypeToPropertyMap();
                var factoryMethod = map.FirstOrDefault(unaryExpression.Type);
                PropertyExpression castedExpr = factoryMethod.Invoke(operand as PropertyExpression);

                if (ekProp == null || ekProp.EktronExpressionType == null)
                {
                    // if the property has no EktronExpressionType set, then we assume
                    // this is a valid cast (user knows best?) and just get the expression
                    // builder for this type or the default one
                    return castedExpr;
                }

                // if the property does have the type set, and the cast doesn't
                // work for that type, then we throw an exception
                if (castedExpr.GetType() != operand.GetType() && unaryExpression.Type != typeof(object))
                {
                    string msg = string.Format(
                        "You said {0} is a {1}, but are trying to cast it to {2}.",
                        memberExpr.Member.Name,
                        ekProp.EktronExpressionType.Name,
                        unaryExpression.Type.Name);
                    throw new InvalidQueryException(msg);
                }

                return operand;
            }

            return operand;
        }

        #endregion
    }
}