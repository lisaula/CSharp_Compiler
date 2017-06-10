using System;

namespace Compiler.Tree
{
    public class CharType : PrimitiveType
    {
        public CharType(Token token) : base(token)
        {
        }
        public CharType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is CharType;
        }
    }
}