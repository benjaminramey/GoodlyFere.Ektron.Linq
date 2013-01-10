#region Usings

using System;
using System.Linq;
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetadataBooleanPropertyAttribute : MetadataPropertyAttribute
    {
        #region Constructors and Destructors

        public MetadataBooleanPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            EktronExpressionType = typeof(BooleanPropertyExpression);
        }

        #endregion
    }
}