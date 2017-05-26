using System.Collections.Generic;

namespace Compiler.Tree
{
    public class MethodNode
    {
        private TypeDefinitionNode type;
        private IdentifierNode id;
        private List<Parameter> parameters;

        public MethodNode(TypeDefinitionNode type, IdentifierNode id, List<Parameter> parameters)
        {
            this.type = type;
            this.id = id;
            this.parameters = parameters;
        }
    }
}