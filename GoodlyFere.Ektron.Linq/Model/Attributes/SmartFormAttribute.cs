#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmartFormAttribute.cs">
// LINQ to Ektron Search, a LINQ interface to the Ektron AdvancedSearchCriteria search engine
// 
// Copyright (C) 2013 Benjamin Ramey
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
// 
// http://www.gnu.org/licenses/lgpl-2.1-standalone.html
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Linq;
using GoodlyFere.Ektron.Linq.IdProviders;
using GoodlyFere.Ektron.Linq.Interfaces;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    /// <summary>
    /// Use this attribute to designate a domain object class as mapping to
    /// a smart form in Ektron.  The SmartFormName property will be passed
    /// to the <see cref="IEktronIdProvider"/> to be mapped to an Ektron
    /// content ID.  Adding this attribute will cause the library to add
    /// a SearchContentProperty.XmlConfigId == SMART_FORM_ID expression to the
    /// AdvancedSearchCriteria.ExpressionTree.
    /// </summary>
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

        /// <summary>
        /// The name of the smart form that this domain object maps to.  This
        /// value can be arbitrary depending on how the supplied <see cref="IEktronIdProvider"/>
        /// handles retrieving Ektron content IDs.  If you use the default <see cref="AppSettingsIdProvider"/>,
        /// then this name will be prepended to "SmartForm" and must match the key of an appSetting
        /// if your application's configuration file.
        /// </summary>
        public string SmartFormName { get; set; }

        #endregion
    }
}