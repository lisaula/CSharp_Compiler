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

        public BoolType(TypeDefinitionNode t) : base(t)
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
    }
}