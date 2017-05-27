using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ConstructorNode
    {
        private EncapsulationNode encapsulation;
        private IdentifierNode id;
        private List<Parameter> parameters;
        private ConstructorInitializerNode base_init;
        private List<Statement> statements;

        public ConstructorNode(EncapsulationNode encapsulation, IdentifierNode id, List<Parameter> parameters, ConstructorInitializerNode base_init, List<Statement> statements)
        {
            this.encapsulation = encapsulation;
            this.id = id;
            this.parameters = parameters;
            this.base_init = base_init;
            this.statements = statements;
        }
    }
}