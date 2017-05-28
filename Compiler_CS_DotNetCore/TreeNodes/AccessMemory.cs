namespace Compiler.Tree
{
    public class AccessMemory : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public Token id;

        public AccessMemory(PrimaryExpressionNode primary, Token id)
        {
            this.primary = primary;
            this.id = id;
        }
        public AccessMemory()
        {
            
        }
    }
}