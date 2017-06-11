using System.Collections.Generic;

namespace Compiler.Tree
{
    public class LocalVariableDefinitionNode : Statement
    {
        public Dictionary<string,FieldNode> variable;

        public LocalVariableDefinitionNode()
        {
            this.variable = new Dictionary<string, FieldNode>();
        }
    }
}