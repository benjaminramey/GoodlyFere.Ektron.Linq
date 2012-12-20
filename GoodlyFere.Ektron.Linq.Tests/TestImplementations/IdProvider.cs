using System.Configuration;
using System.Linq;
using System;
using GoodlyFere.Ektron.Linq.Interfaces;

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