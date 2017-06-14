using System;

namespace Compiler.Tree
{
    public class StringType : PrimitiveType
    {
        public StringType(Token token) : base(token)
        {
        }

        public StringType(TypeDefinitionNode t):base(t)
        {
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return obj is StringType;
        }

        public override Token getPrimaryToken()
        {
            return identifier.token;
        }
    }
}