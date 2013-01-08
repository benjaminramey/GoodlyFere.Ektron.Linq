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