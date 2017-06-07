namespace Compiler.Tree
{
    public class VoidTypeNode : TypeDefinitionNode
    {
        public Token token_type;
        public VoidTypeNode(Token token_type)
        {
            this.token_type = token_type;
        }
        public VoidTypeNode()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}