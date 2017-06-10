namespace Compiler.Tree
{
    public class ModifierNode
    {
        public Token token;

        public ModifierNode(Token token)
        {
            this.token = token;
        }
        public ModifierNode()
        {

        }

        public override string ToString()
        {
            return token.lexema;
        }
    }
}