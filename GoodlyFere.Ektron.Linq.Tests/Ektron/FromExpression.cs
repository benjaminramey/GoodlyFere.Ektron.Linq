#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Ektron
{
    /// <summary>
    ///     Defines an expression representing the "from" clause in a SQL query
    /// </summary>
    internal class FromExpression : Expression
    {
        #region Constructors and Destructors

        public FromExpression()
        {
            WhereExpression = null;
        }

        #endregion

        #region Public Properties

        public WhereExpression WhereExpression { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Accepts a visitor to perform actions on this expression node
        /// </summary>
        /// <param name="visitor"> Acting visitor </param>
        public override void Accept(ExpressionVisitor visitor)
        {
            throw new NotImplementedException("This expression is not implemented for your visitor.");
        }

        /// <summary>
        ///     Accepts a visitor to perform actions on this expression node
        /// </summary>
        /// <param name="visitor"> Acting visitor </param>
        public void Accept(SS2010ExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }
}