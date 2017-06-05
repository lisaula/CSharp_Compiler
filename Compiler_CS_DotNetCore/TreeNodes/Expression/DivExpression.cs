namespace Compiler.Tree
{
    public class DivExpression : BinaryExpression
    {

        public DivExpression(ExpressionNode leftExpression, UnaryExpressionNode rightExpression) : base(leftExpression, rightExpression)
        {
        }
        public DivExpression()
        {

        }
    }
}