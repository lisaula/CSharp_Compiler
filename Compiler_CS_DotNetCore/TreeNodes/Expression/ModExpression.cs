namespace Compiler.Tree
{
    public class ModExpression : BinaryExpression
    {
        public ModExpression(ExpressionNode leftExpression, UnaryExpressionNode rightExpression) : base(leftExpression, rightExpression)
        {
        }
        public ModExpression()
        {

        }
    }
}