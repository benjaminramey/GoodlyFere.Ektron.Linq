#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartFormDatePropertyAttribute : SmartFormPropertyAttribute
    {
        #region Constructors and Destructors

        public SmartFormDatePropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(DatePropertyExpression);
        }

        #endregion
    }
}