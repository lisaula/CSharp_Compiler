namespace Compiler.Tree
{
    public class Parameter
    {
        public TypeDefinitionNode type;
        public IdentifierNode id;

        public Parameter(TypeDefinitionNode t, IdentifierNode id)
        {
            this.type = t;
            this.id = id;
        }
        public Parameter()
        {

        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}