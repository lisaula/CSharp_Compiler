namespace Compiler.Tree
{
    public class DictionaryTypeNode : TypeDefinitionNode
    {
        private TypeDefinitionNode t1;
        private TypeDefinitionNode t2;
        public ArrayNode arrayNode;
        public DictionaryTypeNode(TypeDefinitionNode t1, TypeDefinitionNode t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
    }
}