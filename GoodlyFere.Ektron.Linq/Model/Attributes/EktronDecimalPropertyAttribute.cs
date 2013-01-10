#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronDecimalPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public EktronDecimalPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(DecimalPropertyExpression);
        }

        #endregion
    }
}