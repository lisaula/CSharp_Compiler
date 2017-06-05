using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ConstructorNode
    {
        public EncapsulationNode encapsulation;
        public IdentifierNode id;
        public List<Parameter> parameters;
        public ConstructorInitializerNode base_init;
        public BodyStatement bodyStatements;

        public ConstructorNode(EncapsulationNode encapsulation, IdentifierNode id, List<Parameter> parameters, ConstructorInitializerNode base_init, BodyStatement bodyStatements)
        {
            this.encapsulation = encapsulation;
            this.id = id;
            this.parameters = parameters;
            this.base_init = base_init;
            this.bodyStatements = bodyStatements;
        }
        public ConstructorNode()
        {

        }
    }
}