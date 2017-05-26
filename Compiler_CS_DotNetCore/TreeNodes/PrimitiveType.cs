using Compiler.Tree;

namespace Compiler.Tree
{
    internal class PrimitiveType : TypeDefinitionNode
    {
        private TokenType type;
        public ArrayNode arrayNode;
        public PrimitiveType(TokenType type)
        {
            this.type = type;
        }
    }
}