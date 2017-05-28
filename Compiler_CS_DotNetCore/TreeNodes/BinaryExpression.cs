namespace Compiler.Tree
{
    public class BinaryExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public Token @operator;
        public ExpressionNode rightExpression;

        public BinaryExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.@operator = @operator;
            this.rightExpression = rightExpression;
        }
        public BinaryExpression()
        {

        }
    }
}