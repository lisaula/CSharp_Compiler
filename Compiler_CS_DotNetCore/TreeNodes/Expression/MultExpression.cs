namespace Compiler.Tree
{
    public class MultExpression : BinaryExpression
    {
        public MultExpression()
        {

        }
        public MultExpression(ExpressionNode unary, ExpressionNode rightExpression) : base (unary, rightExpression)
        {
        }
    }
}