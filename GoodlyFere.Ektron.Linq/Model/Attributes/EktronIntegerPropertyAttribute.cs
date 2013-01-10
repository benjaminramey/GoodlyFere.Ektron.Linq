#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronIntegerPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public EktronIntegerPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(IntegerPropertyExpression);
        }

        #endregion
    }
}