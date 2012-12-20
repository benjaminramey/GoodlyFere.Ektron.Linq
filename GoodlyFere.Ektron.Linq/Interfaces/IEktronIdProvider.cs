#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Interfaces
{
    public interface IEktronIdProvider
    {
        #region Public Methods

        long GetContentTypeId(string name);

        long GetSmartFormId(string name);

        #endregion
    }
}