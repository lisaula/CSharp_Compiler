namespace Compiler.Tree
{
    public  class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expr;

        public ParenthesizedExpressionNode(ExpressionNode expr)
        {
            this.expr = expr;
        }
        public ParenthesizedExpressionNode()
        {

        }
    }
}