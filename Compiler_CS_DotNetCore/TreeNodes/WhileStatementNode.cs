namespace Compiler.Tree
{
    public class WhileStatementNode: EmbeddedStatementNode
    {
        public ExpressionNode conditionExpression;
        public EmbeddedStatementNode body;

        public WhileStatementNode()
        {

        }

        public WhileStatementNode(ExpressionNode conditionExpression, EmbeddedStatementNode body)
        {
            this.conditionExpression = conditionExpression;
            this.body = body;
        }
    }
}