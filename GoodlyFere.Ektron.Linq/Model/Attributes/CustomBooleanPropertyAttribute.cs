#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomBooleanPropertyAttribute : CustomPropertyAttribute
    {
        #region Constructors and Destructors

        public CustomBooleanPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(BooleanPropertyExpression);
        }

        #endregion
    }
}