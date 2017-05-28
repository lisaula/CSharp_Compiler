using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ConstructorNode
    {
        public EncapsulationNode encapsulation;
        public IdentifierNode id;
        public List<Parameter> parameters;
        public ConstructorInitializerNode base_init;
        public List<Statement> statements;

        public ConstructorNode(EncapsulationNode encapsulation, IdentifierNode id, List<Parameter> parameters, ConstructorInitializerNode base_init, List<Statement> statements)
        {
            this.encapsulation = encapsulation;
            this.id = id;
            this.parameters = parameters;
            this.base_init = base_init;
            this.statements = statements;
        }
        public ConstructorNode()
        {

        }
    }
}