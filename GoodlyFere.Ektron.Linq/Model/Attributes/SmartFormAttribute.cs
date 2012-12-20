﻿#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SmartFormAttribute : Attribute
    {
        #region Constructors and Destructors

        public SmartFormAttribute(string smartFormName)
        {
            SmartFormName = smartFormName;
        }

        #endregion

        #region Public Properties

        public string SmartFormName { get; set; }

        #endregion
    }
}