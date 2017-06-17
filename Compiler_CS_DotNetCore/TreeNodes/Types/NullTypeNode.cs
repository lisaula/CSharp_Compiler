using Compiler_CS_DotNetCore.Semantic;
using System;
using System.Text;

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
            return obj is NullTypeNode || obj is ClassDefinitionNode && ((ClassDefinitionNode)obj).identifier.token.lexema == Utils.Null;
        }

        public override string getComparativeType()
        {
            return GetType().Name;
        }

        public override void generateCode(StringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}