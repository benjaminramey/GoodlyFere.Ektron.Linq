#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.EktronModel.Attributes
{
    public class FolderPathAttribute : Attribute
    {
        #region Constructors and Destructors

        public FolderPathAttribute(string folderPath)
        {
            FolderPath = folderPath;
        }

        #endregion

        #region Public Properties

        public string FolderPath { get; set; }

        #endregion
    }
}