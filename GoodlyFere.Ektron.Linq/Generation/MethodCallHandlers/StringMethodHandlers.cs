#region Usings

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.MethodCallHandlers
{
    public static class StringMethodHandlers
    {
        #region Public Methods

        public static Ek.Expression HandleStringContains(
            Expression obj, ReadOnlyCollection<Expression> arguments)
        {
            Ek.StringPropertyExpression objectExpr = QueryBuildingVisitor.Build(obj) as Ek.StringPropertyExpression;
            Ek.StringValueExpression argExpr = QueryBuildingVisitor.Build(arguments[0]) as Ek.StringValueExpression;

            return objectExpr.Contains(argExpr.Value, Ek.WordForms.Inflections);
        }

        #endregion
    }
}