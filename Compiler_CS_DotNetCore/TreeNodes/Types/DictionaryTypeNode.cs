namespace Compiler.Tree
{
    public class DictionaryTypeNode : TypeDefinitionNode
    {
        public TypeDefinitionNode t1;
        public TypeDefinitionNode t2;
        public ArrayNode arrayNode;
        public DictionaryTypeNode(TypeDefinitionNode t1, TypeDefinitionNode t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }
        public DictionaryTypeNode()
        {

        }

        public override string ToString()
        {
            return "Dictionary<" + t1.ToString() + "," + t2.ToString() + ">";
        }
    }
}