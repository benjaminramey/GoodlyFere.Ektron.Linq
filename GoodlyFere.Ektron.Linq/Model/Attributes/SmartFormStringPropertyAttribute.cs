#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartFormStringPropertyAttribute : SmartFormPropertyAttribute
    {
        #region Constructors and Destructors

        public SmartFormStringPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(StringPropertyExpression);
        }

        #endregion
    }
}