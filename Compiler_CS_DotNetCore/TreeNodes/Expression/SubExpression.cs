namespace Compiler.Tree
{
    public class SubExpression : BinaryExpression
    {
        public SubExpression()
        {

        }
        public SubExpression(ExpressionNode leftExpression, ExpressionNode rightExpression) : base (leftExpression, rightExpression)
        {
        }
    }
}