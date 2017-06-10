using System;

namespace Compiler.Tree
{
    public class FloatType : PrimitiveType
    {
        public FloatType(Token token) : base(token)
        {
        }
        public FloatType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is FloatType;
        }
    }
}