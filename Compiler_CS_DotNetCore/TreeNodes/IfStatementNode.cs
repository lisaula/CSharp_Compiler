namespace Compiler.Tree
{
    public class IfStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode expr;
        public EmbeddedStatementNode body;
        public ElseStatementNode elseStatement;

        public IfStatementNode()
        {

        }

        public IfStatementNode(ExpressionNode expr, EmbeddedStatementNode body, ElseStatementNode elseStatement)
        {
            this.expr = expr;
            this.body = body;
            this.elseStatement = elseStatement;
        }
    }
}