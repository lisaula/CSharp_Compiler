namespace Compiler.Tree
{
    public class LiteralNode : PrimaryExpressionNode
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