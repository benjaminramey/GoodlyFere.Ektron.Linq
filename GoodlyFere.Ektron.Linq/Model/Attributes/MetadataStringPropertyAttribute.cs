#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetadataStringPropertyAttribute : MetadataPropertyAttribute
    {
        #region Constructors and Destructors

        public MetadataStringPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(StringPropertyExpression);
        }

        #endregion
    }
}