using System;
using System.Text;

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

        public override void generateCode(StringBuilder builder)
        {
            builder.Append("\n " + identifier.ToString() + " : ");
            expressionNode.generateCode(builder);
            builder.Append(",");
        }
    }
}