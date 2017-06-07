using System;

namespace Compiler.Tree
{
    public class IntType : PrimitiveType
    {
        public IntType(Token token) : base(token)
        {
        }
        public IntType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}