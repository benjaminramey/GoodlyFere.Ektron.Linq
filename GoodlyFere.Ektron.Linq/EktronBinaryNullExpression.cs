#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using Ektron.Cms.Search.Expressions;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Parsing;
using Expression = System.Linq.Expressions.Expression;

#endregion

namespace GoodlyFere.Ektron.Linq
{
    internal class EktronBinaryNullExpression : ExtensionExpression
    {
        #region Constructors and Destructors

        public EktronBinaryNullExpression(Type type, PropertyExpression ektronPropertyExpr)
            : base(type)
        {
            EktronProperty = ektronPropertyExpr;
        }

        public EktronBinaryNullExpression(Type type, ExpressionType nodeType, PropertyExpression ektronPropertyExpr)
            : base(type, nodeType)
        {
            EktronProperty = ektronPropertyExpr;
        }

        #endregion

        #region Public Properties

        public PropertyExpression EktronProperty { get; set; }

        #endregion

        #region Methods

        protected override Expression VisitChildren(ExpressionTreeVisitor visitor)
        {
            return this;
        }

        #endregion
    }
}