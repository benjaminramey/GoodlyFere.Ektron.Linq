#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartFormBooleanPropertyAttribute : SmartFormPropertyAttribute
    {
        #region Constructors and Destructors

        public SmartFormBooleanPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(BooleanPropertyExpression);
        }

        #endregion
    }
}