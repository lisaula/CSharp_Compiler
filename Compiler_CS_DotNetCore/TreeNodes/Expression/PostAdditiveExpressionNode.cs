using System;

namespace Compiler.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public Token @operator;

        public PostAdditiveExpressionNode(PrimaryExpressionNode primary, Token @operator)
        {
            this.primary = primary;
            this.@operator = @operator;
        }
        public PostAdditiveExpressionNode()
        {

        }

        public PostAdditiveExpressionNode(Token @operator)
        {
            this.@operator = @operator;
        }

        public override TypeDefinitionNode evaluateType()
        {
            throw new NotImplementedException();
        }
    }
}