#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ek = Ektron.Cms.Search.Expressions;

#endregion

namespace GoodlyFere.Ektron.Linq.Generation.MethodCallHandlers
{
    internal delegate Ek.Expression MethodCallHandler(Expression obj, ReadOnlyCollection<Expression> arguments);

    internal class HandledMethodsMap : Dictionary<MethodInfo, MethodCallHandler>
    {
        #region Constructors and Destructors

        public HandledMethodsMap()
        {
            Add(typeof(string).GetMethod("Contains"), StringMethodHandlers.HandleStringContains);
        }

        #endregion
    }
}