namespace Compiler.Tree
{
    public class EncapsulationNode
    {
        private string lexema;

        public EncapsulationNode()
        {
            lexema = null;
        }

        public EncapsulationNode(string lexema)
        {
            this.lexema = lexema;
        }
    }
}