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
        public BodyStatement bodyStatements;
        public MethodNode(TypeDefinitionNode returnType, IdentifierNode id, List<Parameter> parameters)
        {
            this.returnType = returnType;
            this.id = id;
            this.parameters = parameters;
        }
        public MethodNode()
        {

        }

        public MethodNode(EncapsulationNode encapsulation, ModifierNode modifier, TypeDefinitionNode type, IdentifierNode id, List<Parameter> parameters, BodyStatement bodyStatements)
        {
            this.encapsulation = encapsulation;
            this.modifier = modifier;
            this.returnType = type;
            this.id = id;
            this.parameters = parameters;
            this.bodyStatements = bodyStatements;
        }
    }
}