using System;

namespace Compiler
{
    internal class StateNotFoundException : Exception
    {
        public StateNotFoundException()
        {
        }

        public StateNotFoundException(string message) : base(message)
        {
        }

        public StateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}