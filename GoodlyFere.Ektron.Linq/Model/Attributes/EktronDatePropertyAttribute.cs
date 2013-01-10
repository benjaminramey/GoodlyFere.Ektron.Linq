#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronDatePropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public EktronDatePropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(DatePropertyExpression);
        }

        #endregion
    }
}