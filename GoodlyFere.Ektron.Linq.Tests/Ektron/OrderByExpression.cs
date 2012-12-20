#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using Ektron.Cms.Search.SS2010;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Ektron
{
    /// <summary>
    ///     Defines an expression representing an "order-by" clause for a SQL query
    /// </summary>
    internal class OrderByExpression : Expression
    {
        #region Constructors and Destructors

        public OrderByExpression(List<OrderData> orders)
        {
            if (orders != null)
            {
                Orders = orders;
                return;
            }
            else
            {
                throw new ArgumentNullException("orders");
            }
        }

        #endregion

        #region Public Properties

        public List<OrderData> Orders { get; set; }

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
        ///     Accepts a SS2008 specific visitor to perform actions on this expression node
        /// </summary>
        /// <param name="visitor"> Acting visitor </param>
        public void Accept(SS2010ExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion
    }
}