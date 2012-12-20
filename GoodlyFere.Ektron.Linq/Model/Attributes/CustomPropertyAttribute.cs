#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public CustomPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            IsMetadataProperty = false;
            IsCustomProperty = true;
            IsSmartFormProperty = false;
        }

        #endregion
    }
}