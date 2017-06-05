namespace Compiler.Tree
{
    public class RelationalExpression : ConditionExpression
    {
        public Token @operator;

        public RelationalExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression):base(leftExpression, rightExpression)
        {
            this.@operator = @operator;
        }
        public RelationalExpression()
        {

        }
    }
}