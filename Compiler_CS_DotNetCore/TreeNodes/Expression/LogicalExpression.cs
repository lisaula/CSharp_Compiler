namespace Compiler.Tree
{
    public class LogicalExpression : ConditionExpression
    {
        public Token Operator;
        public LogicalExpression(ExpressionNode condition, Token Operator, ExpressionNode conditionExpression) : base(condition, conditionExpression)
        {
            this.Operator = Operator;
            rules[""] = new BoolType();
        }
        public LogicalExpression()
        {

        }
    }
}