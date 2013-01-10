#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronBooleanPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public EktronBooleanPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(BooleanPropertyExpression);
        }

        #endregion
    }
}