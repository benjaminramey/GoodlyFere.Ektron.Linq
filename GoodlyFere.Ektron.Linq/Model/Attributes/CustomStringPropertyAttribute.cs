#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomStringPropertyAttribute : CustomPropertyAttribute
    {
        #region Constructors and Destructors

        public CustomStringPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(StringPropertyExpression);
        }

        #endregion
    }
}