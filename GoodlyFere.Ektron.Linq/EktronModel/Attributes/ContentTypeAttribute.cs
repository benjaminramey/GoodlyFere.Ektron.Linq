#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.EktronModel.Attributes
{
    public class ContentTypeAttribute : Attribute
    {
        #region Constructors and Destructors

        public ContentTypeAttribute(string contentTypeName)
        {
            ContentTypeName = contentTypeName;
            ContentSubTypeName = string.Empty;
        }

        public ContentTypeAttribute(string contentTypeName, string contentSubTypeName)
        {
            ContentTypeName = contentTypeName;
            ContentSubTypeName = contentSubTypeName;
        }

        #endregion

        #region Public Properties

        public string ContentSubTypeName { get; set; }
        public string ContentTypeName { get; set; }

        public bool HasSubType
        {
            get
            {
                return !string.IsNullOrEmpty(ContentSubTypeName);
            }
        }

        #endregion
    }
}