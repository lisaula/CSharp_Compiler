namespace Compiler.Tree
{
    public abstract class LiteralNode : PrimaryExpressionNode
    {
        public Token token;

        public LiteralNode(Token token)
        {
            this.token = token;
        }
        public LiteralNode()
        {

        }
    }
}