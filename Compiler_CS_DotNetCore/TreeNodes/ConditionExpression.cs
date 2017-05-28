namespace Compiler.Tree
{
    public class ConditionExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public Token Operator;
        public ExpressionNode rightExpression;
        public TypeDefinitionNode type;

        public ConditionExpression(ExpressionNode leftExpression, Token Operator, ExpressionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.Operator = Operator;
            this.rightExpression = rightExpression;
        }
        public ConditionExpression(ExpressionNode leftExpression, Token Operator, TypeDefinitionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.Operator = Operator;
            this.type = rightExpression;
        }
        public ConditionExpression()
        {

        }
    }
}