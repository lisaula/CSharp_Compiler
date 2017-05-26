namespace Compiler.Tree
{
    internal class VoidTypeNode : TypeDefinitionNode
    {
        private TokenType type;
        public ArrayNode arrayNode;
        public VoidTypeNode(TokenType type)
        {
            this.type = type;
        }

        public VoidTypeNode(TokenType type, ArrayNode array)
        {
            this.type = type;
            arrayNode = array;
        }
    }
}