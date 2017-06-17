namespace Compiler.Tree
{
    public class Parameter
    {
        public TypeDefinitionNode type;
        public IdentifierNode id;
        public TypeDefinitionNode primaryType;
        public Parameter(TypeDefinitionNode t, IdentifierNode id):this()
        {
            this.type = t;
            this.id = id;
        }
        public Parameter()
        {
            primaryType = null;
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}