using Compiler.Tree;

namespace Compiler.Tree
{
    public class FieldNode
    {
        private EncapsulationNode encapsulation;
        private ModifierNode modifier;
        private TypeDefinitionNode type;
        private IdentifierNode id;
        private VariableInitializer assignment;

        public FieldNode(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, VariableInitializer assignment)
        {
            this.encapsulation = encapsulation;
            this.modifier = modifier;
            this.type = type;
            this.id = id;
            this.assignment = assignment;
        }
    }
}