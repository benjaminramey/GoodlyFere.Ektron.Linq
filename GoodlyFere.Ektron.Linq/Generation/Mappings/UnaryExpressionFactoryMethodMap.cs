#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal delegate Ek.Expression UnaryExpressionFactoryMethod(Ek.Expression operand);

    internal class UnaryExpressionFactoryMethodMap : Dictionary<ExpressionType, UnaryExpressionFactoryMethod>
    {
        #region Constructors and Destructors

        public UnaryExpressionFactoryMethodMap()
        {
            Add(ExpressionType.Not, operand => new Ek.NotExpression(operand));
        }

        #endregion
    }
}