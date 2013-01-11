#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdProvider.cs">
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
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System.Configuration;
using System.Linq;
using System;
using GoodlyFere.Ektron.Linq.Interfaces;

#endregion

namespace GoodlyFere.Ektron.Linq.Tests.TestImplementations
{
    internal class IdProvider : IEktronIdProvider
    {
        #region Public Methods

        public long GetContentTypeId(string name)
        {
            EnsureValidName(name);
            string key = string.Concat(name, "ContentType");
            return GetId(key);
        }

        public long GetSmartFormId(string name)
        {
            EnsureValidName(name);
            string key = string.Concat(name, "SmartForm");
            return GetId(key);
        }

        #endregion

        #region Methods

        private static long GetId(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(
                    "key", string.Format("Could not find {0} in application settings.", key));
            }

            return long.Parse(value);
        }

        private void EnsureValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
        }

        #endregion
    }
}