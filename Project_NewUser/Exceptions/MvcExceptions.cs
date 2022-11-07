using System;
using System.Globalization;

namespace Project_NewUser.Exceptions
{
    public class MvcExceptions : Exception
    {
        public MvcExceptions() { }

        public MvcExceptions(string message) : base(message) { }

        public MvcExceptions(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { } 
    }
}
