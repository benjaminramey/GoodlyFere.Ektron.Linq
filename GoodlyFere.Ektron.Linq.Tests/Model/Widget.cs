#region Usings

using System;
using System.Linq;
using GoodlyFere.Ektron.Linq.Model;
using GoodlyFere.Ektron.Linq.Model.Attributes;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.Model
{
    public class Widget
    {
        #region Public Properties

        public string Name { get; set; }

        [EktronProperty(EkConstants.SearchProperties.ContentId)]
        public long Id { get; set; }

        #endregion
    }
}