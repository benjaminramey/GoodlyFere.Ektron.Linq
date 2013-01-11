#region License

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace GoodlyFere.Ektron.Linq.Extensions
{
    public static class ReflectionExtensions
    {
        #region Constants and Fields

        private static readonly Dictionary<string, object> CustomAttributes;

        #endregion

        #region Constructors and Destructors

        static ReflectionExtensions()
        {
            CustomAttributes = new Dictionary<string, object>();
        }

        #endregion

        #region Public Methods

        public static T GetCustomAttribute<T>(this Type type) where T : Attribute
        {
            if (type == null || string.IsNullOrEmpty(type.FullName))
            {
                return null;
            }

            string key = GetKey<T>(type);
            if (CustomAttributes.ContainsKey(key))
            {
                return CustomAttributes[key] as T;
            }

            CustomAttributes.Add(key, type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T);
            return CustomAttributes[key] as T;
        }

        public static T GetCustomAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            if (memberInfo == null)
            {
                return null;
            }

            string key = GetKey<T>(memberInfo);
            if (CustomAttributes.ContainsKey(key))
            {
                return CustomAttributes[key] as T;
            }

            CustomAttributes.Add(key, memberInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T);
            return CustomAttributes[key] as T;
        }

        #endregion

        #region Methods

        private static string GetKey<T>(Type type)
        {
            return string.Concat(type.FullName, "!!!~!!!", typeof(T).FullName);
        }

        private static string GetKey<T>(MemberInfo memberInfo)
        {
            return string.Concat(
                memberInfo.ReflectedType.FullName, "!!!~!!!", memberInfo.Name, "!!!~!!!", typeof(T).FullName);
        }

        #endregion
    }
}