#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Ektron.Linq.Generation.ExpressionHandlers;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal delegate Ek.Expression UnaryExpressionFactoryMethod(UnaryExpression unaryExpression, Ek.Expression operand);

    internal class UnaryExpressionMap : Dictionary<ExpressionType, UnaryExpressionFactoryMethod>
    {
        #region Constructors and Destructors

        public UnaryExpressionMap()
        {
            Add(ExpressionType.Not, (ue, operand) => new Ek.NotExpression(operand));
            Add(ExpressionType.Convert, UnaryExpressionHandlers.HandleConvert);
        }

        #endregion
    }
}