namespace Compiler.Tree
{
    public  class DoStatementNode : EmbeddedStatementNode
    {
        public EmbeddedStatementNode body;
        public ExpressionNode conditionExpression;

        public DoStatementNode()
        {

        }

        public DoStatementNode(EmbeddedStatementNode body, ExpressionNode conditionExpression)
        {
            this.body = body;
            this.conditionExpression = conditionExpression;
        }
    }

}