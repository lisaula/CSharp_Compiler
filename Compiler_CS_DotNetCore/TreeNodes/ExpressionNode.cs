namespace Compiler.Tree
{
    public class ExpressionNode : VariableInitializer
    {
        public string v;
        public ExpressionNode()
        {

        }
        public ExpressionNode(string v)
        {
            this.v = v;
        }
    }
}