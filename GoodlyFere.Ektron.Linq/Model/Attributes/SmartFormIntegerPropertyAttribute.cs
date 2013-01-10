#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartFormIntegerPropertyAttribute : SmartFormPropertyAttribute
    {
        #region Constructors and Destructors

        public SmartFormIntegerPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(IntegerPropertyExpression);
        }

        #endregion
    }
}