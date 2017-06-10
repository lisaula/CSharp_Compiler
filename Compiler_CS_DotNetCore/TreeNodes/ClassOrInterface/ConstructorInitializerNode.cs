using System.Collections.Generic;

namespace Compiler.Tree
{
    public class ConstructorInitializerNode
    {
        public List<ExpressionNode> argumentList;
        public Token reference;
        public ConstructorInitializerNode()
        {

        }

        public ConstructorInitializerNode(Token reference, List<ExpressionNode> argumentList)
        {
            this.reference = reference;
            this.argumentList = argumentList;
        }
    }
}