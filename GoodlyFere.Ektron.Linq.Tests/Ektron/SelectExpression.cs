#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Ektron
{
    internal class SelectExpression : Expression
    {
        #region Constructors and Destructors

        private SelectExpression(IEnumerable<PropertyExpression> properties)
        {
            if (properties != null)
            {
                Properties = new List<PropertyExpression>(properties);
                FromExpression = null;
                WhereExpression = null;
                return;
            }
            else
            {
                throw new ArgumentNullException("properties");
            }
        }

        #endregion

        #region Public Properties

        public FromExpression FromExpression { get; set; }

        public OrderByExpression OrderByExpression { get; set; }

        public List<PropertyExpression> Properties { get; set; }

        public WhereExpression WhereExpression { get; set; }

        #endregion

        #region Public Methods

        public static SelectExpression Select(IEnumerable<PropertyExpression> properties)
        {
            return new SelectExpression(properties);
        }

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

        public SelectExpression From()
        {
            FromExpression = new FromExpression();
            return this;
        }

        public SelectExpression OrderBy(List<OrderData> ordering)
        {
            OrderByExpression = new OrderByExpression(ordering);
            return this;
        }

        public SelectExpression Where(Expression tree)
        {
            WhereExpression = new WhereExpression(tree);
            return this;
        }

        #endregion
    }
}