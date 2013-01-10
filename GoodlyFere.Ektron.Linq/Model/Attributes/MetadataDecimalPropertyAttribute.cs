#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetadataDecimalPropertyAttribute : MetadataPropertyAttribute
    {
        #region Constructors and Destructors

        public MetadataDecimalPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(DecimalPropertyExpression);
        }

        #endregion
    }
}