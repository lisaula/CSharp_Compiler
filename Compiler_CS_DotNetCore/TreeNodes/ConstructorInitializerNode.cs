using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ConstructorInitializerNode
    {
        private List<ExpressionNode> argumentList;

        public ConstructorInitializerNode(List<ExpressionNode> argumentList)
        {
            this.argumentList = argumentList;
        }
    }
}