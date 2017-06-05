namespace Compiler.Tree
{
    public class ElseStatementNode
    {
        public EmbeddedStatementNode body;

        public ElseStatementNode()
        {

        }

        public ElseStatementNode(EmbeddedStatementNode body)
        {
            this.body = body;
        }
    }
}