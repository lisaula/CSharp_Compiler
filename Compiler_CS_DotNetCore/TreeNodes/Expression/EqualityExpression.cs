namespace Compiler.Tree
{
    public class EqualityExpression : ConditionExpression
    {
        public Token @operator;

        public EqualityExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression): base(leftExpression, rightExpression)
        {
            this.@operator = @operator;

        }
        public EqualityExpression()
        {

        }
    }
}