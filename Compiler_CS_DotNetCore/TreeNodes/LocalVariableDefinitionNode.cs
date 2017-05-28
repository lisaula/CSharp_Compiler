using System.Collections.Generic;

namespace Compiler.Tree
{
    public class LocalVariableDefinitionNode : Statement
    {
        public TypeDefinitionNode type;
        public List<FieldNode> variable;

        public LocalVariableDefinitionNode(TypeDefinitionNode type, List<FieldNode> variables)
        {
            this.type = type;
            this.variable = variables;
        }
        public LocalVariableDefinitionNode()
        {

        }
    }
}