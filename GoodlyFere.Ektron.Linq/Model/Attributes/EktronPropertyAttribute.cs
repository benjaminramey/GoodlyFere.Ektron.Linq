#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronPropertyAttribute : Attribute
    {
        #region Constructors and Destructors

        public EktronPropertyAttribute(string ektronPropertyName)
        {
            EktronPropertyName = ektronPropertyName;
        }

        #endregion

        #region Public Properties

        public Type EktronExpressionType { get; set; }
        public string EktronPropertyName { get; set; }
        public bool IsCustomProperty { get; set; }
        public bool IsMetadataProperty { get; set; }
        public bool IsSmartFormProperty { get; set; }

        #endregion
    }
}