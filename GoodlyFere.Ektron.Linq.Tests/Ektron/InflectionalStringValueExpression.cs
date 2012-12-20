#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Ektron
{
    public class InflectionalStringValueExpression : ValueExpression<string>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="value">String value</param>
        public InflectionalStringValueExpression(string value)
        {
            base.Value = value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Accepts a visitor to perform actions on this expression node
        /// </summary>
        /// <param name="visitor">Acting visitor</param>
        public override void Accept(ExpressionVisitor visitor)
        {
            SS2010ExpressionVisitor sS2010ExpressionVisitor = visitor as SS2010ExpressionVisitor;
            if (sS2010ExpressionVisitor != null)
            {
                sS2010ExpressionVisitor.Visit(this);
                return;
            }
            else
            {
                throw new NotImplementedException("This expression is not implemented for your visitor.");
            }
        }

        #endregion
    }
}