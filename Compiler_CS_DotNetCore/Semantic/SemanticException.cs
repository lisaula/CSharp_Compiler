﻿using System;

namespace Compiler_CS_DotNetCore.Semantic
{
    internal class SemanticException : Exception
    {
        private string file;

        public SemanticException()
        {
        }

        public SemanticException(string message) : base(message)
        {
        }

        public SemanticException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SemanticException(string message, string file) : this(file+": "+message)
        {
        }
    }
}