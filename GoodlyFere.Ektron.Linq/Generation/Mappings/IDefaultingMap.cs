#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.Mappings
{
    internal interface IDefaultingMap<in TArg, out TResult>
    {
        #region Public Properties

        TResult Default { get; }

        #endregion

        #region Public Methods

        TResult FirstOrDefault(TArg key);

        #endregion
    }
}