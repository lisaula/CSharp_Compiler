namespace Compiler.Tree
{
    public class IdenfierNode
    {
        private Token current_token;
        private string lexema;

        public IdenfierNode(string lexema)
        {
            this.lexema = lexema;
        }
    }
}