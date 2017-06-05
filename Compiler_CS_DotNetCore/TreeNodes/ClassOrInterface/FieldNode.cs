using Compiler.Tree;

namespace Compiler.Tree
{
    public class FieldNode
    {
        public EncapsulationNode encapsulation;
        public ModifierNode modifier;
        public TypeDefinitionNode type;
        public IdentifierNode id;
        public VariableInitializer assignment;

        public FieldNode(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, VariableInitializer assignment)
        {
            this.encapsulation = encapsulation;
            this.modifier = modifier;
            this.type = type;
            this.id = id;
            this.assignment = assignment;
        }
        public FieldNode()
        {

        }
    }
}