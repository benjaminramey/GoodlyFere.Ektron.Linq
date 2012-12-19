#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.EktronModel.Attributes
{
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