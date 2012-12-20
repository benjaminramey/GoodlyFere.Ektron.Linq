#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Ektron.Linq.Exceptions
{
    public class InvalidQueryException : Exception
    {
        #region Constants and Fields

        private readonly string _message;

        #endregion

        #region Constructors and Destructors

        public InvalidQueryException(string message)
        {
            _message = message + " ";
        }

        #endregion

        #region Public Properties

        public override string Message
        {
            get
            {
                return "The client query is invalid: " + _message;
            }
        }

        #endregion
    }
}