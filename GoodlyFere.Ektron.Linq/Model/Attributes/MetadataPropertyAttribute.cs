#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MetadataPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public MetadataPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            IsMetadataProperty = true;
            IsCustomProperty = false;
            IsSmartFormProperty = false;
        }

        #endregion
    }
}