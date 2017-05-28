using System.Collections.Generic;

namespace Compiler.Tree
{
    public class MethodNode
    {
        public TypeDefinitionNode returnType;
        public IdentifierNode id;
        public List<Parameter> parameters;
        public EncapsulationNode encapsulation;
        public ModifierNode modifier;
        public List<Statement> statements;

        public MethodNode(TypeDefinitionNode returnType, IdentifierNode id, List<Parameter> parameters)
        {
            this.returnType = returnType;
            this.id = id;
            this.parameters = parameters;
        }
        public MethodNode()
        {

        }

        public MethodNode(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, List<Parameter> parameters, List<Statement> statements)
        {
            this.encapsulation = encapsulation;
            this.modifier = modifier;
            this.returnType = type;
            this.id = id;
            this.parameters = parameters;
            this.statements = statements;
        }
    }
}