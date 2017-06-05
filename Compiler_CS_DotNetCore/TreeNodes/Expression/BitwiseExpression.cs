namespace Compiler.Tree
{
    public class BitwiseExpression : BinaryExpression
    {
        public Token @operator;

        public BitwiseExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression): base (leftExpression, rightExpression)
        {
            this.@operator = @operator;
        }
        public BitwiseExpression()
        {

        }
    }
}