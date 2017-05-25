using System;

namespace Compiler.Tree
{
    public class EnumNode
    {
        private IdentifierNode identifier;
        private ExpressionNode expressionNode;

        public EnumNode(IdentifierNode identifier, ExpressionNode expressionNode)
        {
            this.identifier = identifier;
            this.expressionNode = expressionNode;
        }
    }
}