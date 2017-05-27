namespace Compiler.Tree
{
    public class Parameter
    {
        private TypeDefinitionNode type;
        private IdentifierNode id;

        public Parameter(TypeDefinitionNode t, IdentifierNode id)
        {
            this.type = t;
            this.id = id;
        }
    }
}