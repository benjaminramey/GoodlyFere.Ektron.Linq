#region Usings

using System.Configuration;
using System.Linq;
using System;
using GoodlyFere.Ektron.Linq.Interfaces;

#endregion

namespace GoodlyFere.Ektron.Linq.Samples
{
    public class AppSettingsIdProvider : IEktronIdProvider
    {
        #region Public Methods

        public long GetContentTypeId(string name)
        {
            EnsureValidName(name);
            string key = String.Concat(name, "ContentType");
            return GetId(key);
        }

        public long GetSmartFormId(string name)
        {
            EnsureValidName(name);
            string key = String.Concat(name, "SmartForm");
            return GetId(key);
        }

        #endregion

        #region Methods

        private static long GetId(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentOutOfRangeException(
                    "key", String.Format("Could not find {0} in application settings.", key));
            }

            return Int64.Parse(value);
        }

        private void EnsureValidName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
        }

        #endregion
    }
}