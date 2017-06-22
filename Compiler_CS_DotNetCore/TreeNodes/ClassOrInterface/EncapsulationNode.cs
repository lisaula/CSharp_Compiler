namespace Compiler.Tree
{
    public class EncapsulationNode
    {
        public Token token;

        public EncapsulationNode()
        {
            token = new Token();
            token.type = TokenType.RW_PRIVATE;
        }

        public EncapsulationNode(Token token)
        {
            this.token = token;
        }
    }
}