using Compiler.Tree;

namespace Compiler.Tree
{
    internal class PrimitiveType : TypeDefinitionNode
    {
        private Token token_type;
        public ArrayNode arrayNode;
        public PrimitiveType(Token token)
        {
            this.token_type = token;
        }
    }
}