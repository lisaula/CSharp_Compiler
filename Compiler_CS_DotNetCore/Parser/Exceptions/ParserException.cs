using System;

namespace Compiler
{
    public class ParserException : Exception
    {
        public ParserException()
        {
        }

        public ParserException(string message) : base(message)
        {
        }

        public ParserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ParserException(string filename,string expected, int row, int column)
        {
            string message = filename+":"+"Parser Exception: expected \"" + expected + "\" in line " + row + " and column " + column;
            throw new ParserException(message);
        }
    }
}