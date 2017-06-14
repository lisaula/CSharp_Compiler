using System;

namespace Compiler.Tree
{
    public class BoolType : PrimitiveType
    {
        public BoolType(Token token) : base(token)
        {
        }
        public BoolType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is BoolType;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }

        public override string GetComparativeType()
        {
            throw new NotImplementedException();
        }
    }
}