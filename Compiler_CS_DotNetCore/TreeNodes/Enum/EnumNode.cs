using System;

namespace Compiler.Tree
{
    public class EnumNode
    {
        public IdentifierNode identifier;
        public ExpressionNode expressionNode;

        public EnumNode(IdentifierNode identifier, ExpressionNode expressionNode)
        {
            this.identifier = identifier;
            this.expressionNode = expressionNode;
        }
        public EnumNode()
        {

        }
    }
}