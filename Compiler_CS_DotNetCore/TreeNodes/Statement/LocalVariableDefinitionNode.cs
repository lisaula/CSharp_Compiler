using System.Collections.Generic;

namespace Compiler.Tree
{
    public class LocalVariableDefinitionNode : Statement
    {
        public List<FieldNode> variable;

        public LocalVariableDefinitionNode(List<FieldNode> variables)
        {
            this.variable = variables;
        }
        public LocalVariableDefinitionNode()
        {

        }
    }
}