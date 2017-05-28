namespace Compiler.Tree
{
    public class StatementExpressionNode: EmbeddedStatementNode
    {
        public ExpressionNode expression;
        public StatementExpressionNode()
        {

        }
        public StatementExpressionNode(ExpressionNode expression)
        {
            this.expression = expression;
        }
    }
}