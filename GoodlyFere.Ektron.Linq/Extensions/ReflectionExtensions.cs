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