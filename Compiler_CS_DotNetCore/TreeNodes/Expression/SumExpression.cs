namespace Compiler.Tree
{
    public class SumExpression : BinaryExpression
    {

        public SumExpression(ExpressionNode leftExpression, ExpressionNode rightExpression) : base(leftExpression, rightExpression)
        {
        }
        public SumExpression()
        {

        }
    }
}