namespace Compiler.Tree
{
    public class ModifierNode
    {
        public Token token;
        public bool evaluated { get; set; }
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