namespace Compiler.Tree
{
    public class Parameter
    {
        private TypeDefinitionNode t;
        private IdentifierNode id;

        public Parameter(TypeDefinitionNode t, IdentifierNode id)
        {
            this.t = t;
            this.id = id;
        }
    }
}