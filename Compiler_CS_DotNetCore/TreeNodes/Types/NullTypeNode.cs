using System;

namespace Compiler.Tree
{
    internal class NullTypeNode : TypeDefinitionNode
    {
        private Token current_token;

        public NullTypeNode(Token current_token)
        {
            this.current_token = current_token;
        }

        public NullTypeNode()
        {
        }

        public override Token getPrimaryToken()
        {
            return current_token;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals(object obj)
        {
            return (obj is NullTypeNode) ;
        }

        public override string getComparativeType()
        {
            return GetType().Name;
        }
    }
}