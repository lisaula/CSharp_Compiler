using System;

namespace Compiler.Tree
{
    public class EnumNode : EnumDefinitionNode
    {
        public new IdentifierNode identifier;
        public ExpressionNode expressionNode;

        public EnumNode(IdentifierNode identifier, ExpressionNode expressionNode)
        {
            this.identifier = identifier;
            this.expressionNode = expressionNode;
        }
        public EnumNode()
        {

        }
        public override string ToString()
        {
            return identifier.ToString();
        }
    }
}