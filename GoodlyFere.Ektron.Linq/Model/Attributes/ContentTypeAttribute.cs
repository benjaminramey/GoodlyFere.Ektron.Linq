#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentTypeAttribute.cs">
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
using GoodlyFere.Ektron.Linq.Interfaces;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    /// <summary>
    ///     Use this property to designate a domain object as mapping
    ///     to a particular Ektron content type and (optionally) a sub-content type.
    ///     The library will use an <see cref="IEktronIdProvider" /> to convert the
    ///     ContentTypeName and ContentSubTypeName into Ektron content IDs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
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

        /// <summary>
        ///     The name of the sub content type to which this domain object maps.
        ///     This name can be arbitrary depending on how your <see cref="IEktronIdProvider" />
        ///     interpets content type names.
        /// </summary>
        public string ContentSubTypeName { get; set; }

        /// <summary>
        ///     The name of the content type to which this domain object maps.
        ///     This name can be arbitrary depending on how your <see cref="IEktronIdProvider" />
        ///     interpets content type names.
        /// </summary>
        public string ContentTypeName { get; set; }

        /// <summary>
        ///     True if a ContentSubTypeName was set.
        /// </summary>
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