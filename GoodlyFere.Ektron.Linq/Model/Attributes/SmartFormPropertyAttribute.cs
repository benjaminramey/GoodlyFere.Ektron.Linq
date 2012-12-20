#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.EktronModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartFormPropertyAttribute : EktronPropertyAttribute
    {
        #region Constructors and Destructors

        public SmartFormPropertyAttribute(string ektronPropertyName)
            : base(ektronPropertyName)
        {
            IsCustomProperty = false;
            IsMetadataProperty = false;
            IsSmartFormProperty = true;
        }

        #endregion
    }
}