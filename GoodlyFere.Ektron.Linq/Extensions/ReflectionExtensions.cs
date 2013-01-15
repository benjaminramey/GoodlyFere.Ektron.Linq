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
// 
// You can contact me at ben.ramey@gmail.com.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#endregion

#region Usings

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using Common.Logging;

#endregion

namespace GoodlyFere.Ektron.Linq.Extensions
{
    public static class ReflectionExtensions
    {
        #region Constants and Fields

        private static readonly MemoryCache AttributeCache;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReflectionExtensions));
        private static readonly CacheItemPolicy Policy;

        #endregion

        #region Constructors and Destructors

        static ReflectionExtensions()
        {
            AttributeCache = MemoryCache.Default;
            Policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, 1, 0, 0) };
        }

        #endregion

        #region Public Methods

        public static T GetCustomAttribute<T>(this Type type) where T : Attribute
        {
            if (type == null || string.IsNullOrEmpty(type.FullName))
            {
                return null;
            }

            Log.DebugFormat("Getting custom attribute for: {0}", type.Name);

            string key = GetKey<T>(type);
            return GetFromCache(key, () => type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T);
        }

        public static T GetCustomAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            if (memberInfo == null)
            {
                return null;
            }

            Log.DebugFormat("Getting custom attribute for: {0}", memberInfo.Name);

            string key = GetKey<T>(memberInfo);
            return GetFromCache(key, () => memberInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T);
        }

        #endregion

        #region Methods

        private static T GetFromCache<T>(string key, Func<T> getAttribute) where T : Attribute
        {
            T attribute = AttributeCache[key] as T;
            if (attribute == null)
            {
                attribute = getAttribute.Invoke();
                if (attribute != null)
                {
                    AttributeCache.Set(key, attribute, Policy);
                }
            }

            return attribute;
        }

        private static string GetKey<T>(Type type)
        {
            string key = string.Concat(type.FullName, "!!!~!!!", typeof(T).FullName);
            Log.DebugFormat("Getting key: {0}", key);
            return key;
        }

        private static string GetKey<T>(MemberInfo memberInfo)
        {
            string key = string.Concat(
                memberInfo.ReflectedType.FullName, "!!!~!!!", memberInfo.Name, "!!!~!!!", typeof(T).FullName);
            Log.DebugFormat("Getting key: {0}", key);
            return key;
        }

        #endregion
    }
}