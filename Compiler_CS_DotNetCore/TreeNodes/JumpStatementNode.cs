namespace Compiler.Tree
{
    public class JumpStatementNode : EmbeddedStatementNode
    {
        public Token token;
        public ExpressionNode expression;
        public JumpStatementNode(Token token)
        {
            this.token = token;
        }

        public JumpStatementNode(Token token, ExpressionNode expression) : this(token)
        {
            this.expression = expression;
        }
    }
}