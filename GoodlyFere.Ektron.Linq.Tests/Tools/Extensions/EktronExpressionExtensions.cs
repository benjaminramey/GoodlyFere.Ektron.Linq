#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Tools.Extensions
{
    internal static class EktronExpressionExtensions
    {
        #region Public Methods

        public static string ToTestString(this Expression expression)
        {
            var visitor = new TestExpressionVisitor();
            expression.Accept(visitor);
            return visitor.ToString();
        }

        #endregion
    }
}