using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ClassInstantiation : InstanceExpressionNode
    {
        public TypeDefinitionNode type;
        public List<ExpressionNode> arguments;

        public ClassInstantiation(TypeDefinitionNode type, List<ExpressionNode> arguments)
        {
            this.type = type;
            this.arguments = arguments;
        }
        public ClassInstantiation()
        {

        }
    }
}