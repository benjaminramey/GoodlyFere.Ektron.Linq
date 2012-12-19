#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.EktronModel.Attributes
{
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