namespace Compiler.Tree
{
    public class EnumDefinitionNode : TypeDefinitionNode
    {
        private EncapsulationNode encapsulation;
        private IdentifierNode identifier;

        public EnumDefinitionNode(EncapsulationNode encapsulation, IdentifierNode identifier)
        {
            this.encapsulation = encapsulation;
            this.identifier = identifier;
        }
    }
}