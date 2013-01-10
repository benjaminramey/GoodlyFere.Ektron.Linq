#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronStringPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public EktronStringPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(StringPropertyExpression);
        }

        #endregion
    }
}