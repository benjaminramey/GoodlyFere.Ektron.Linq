#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EktronPropertyAttribute.cs">
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
using Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Model.Attributes
{
    /// <summary>
    ///     This is the base class from which all other *PropertyAttributes derive. Use this
    ///     attribute to fine-tune how the library translates your domain object properties
    ///     into the AdvancedSearchCriteria.ExpressionTree.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EktronPropertyAttribute : Attribute
    {
        #region Constants and Fields

        private Type _ektronExpressionType;

        #endregion

        #region Constructors and Destructors

        public EktronPropertyAttribute(string ektronPropertyName)
        {
            EktronPropertyName = ektronPropertyName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Defines what Ektron PropertyExpression this property will
        ///     translate into.
        /// </summary>
        public Type EktronExpressionType
        {
            get
            {
                return _ektronExpressionType;
            }
            set
            {
                if (!typeof(PropertyExpression).IsAssignableFrom(value))
                {
                    throw new ArgumentOutOfRangeException(
                        "value", string.Format("Type must be a child class of {0}", typeof(PropertyExpression).FullName));
                }

                _ektronExpressionType = value;
            }
        }

        /// <summary>
        ///     The name of the Ektron property that this domain object property
        ///     should translate into.  If IsSmartFormProperty is set, this must
        ///     be an absolute XML path to the SmartForm element.
        /// </summary>
        public string EktronPropertyName { get; set; }

        /// <summary>
        ///     Defines whether this property is a custom Ektron property.  Mutually
        ///     exclusive with IsMetadataProperty and IsSmartFormProperty.
        /// </summary>
        public bool IsCustomProperty { get; set; }

        /// <summary>
        ///     Defines whether this property is a metadata Ektron property.  Mutually
        ///     exclusive with IsCustomProperty and IsSmartFormProperty.
        /// </summary>
        public bool IsMetadataProperty { get; set; }

        /// <summary>
        ///     Defines whether this property is a smart form Ektron property.  Mutually
        ///     exclusive with IsMetadataProperty and IsCustomProperty.
        /// </summary>
        public bool IsSmartFormProperty { get; set; }

        #endregion
    }
}