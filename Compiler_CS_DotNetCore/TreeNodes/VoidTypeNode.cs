namespace Compiler.Tree
{
    internal class VoidTypeNode : TypeDefinitionNode
    {
        private Token token_type;
        public ArrayNode arrayNode;
        public VoidTypeNode(Token token_type)
        {
            this.token_type = token_type;
        }

        public VoidTypeNode(Token token_type, ArrayNode array)
        {
            this.token_type = token_type;
            arrayNode = array;
        }
    }
}