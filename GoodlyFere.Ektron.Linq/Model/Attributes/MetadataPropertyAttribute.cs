﻿#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetadataPropertyAttribute.cs">
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

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    /// <summary>
    ///     Use this attribute to designate a metadata property.  This is a
    ///     convenience attribute for setting IsMetadataProperty on an <see cref="EktronPropertyAttribute" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
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