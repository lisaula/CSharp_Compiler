namespace Compiler.Tree
{
    public class ReferenceAccessNode : PrimaryExpressionNode
    {
        public  Token token;

        public ReferenceAccessNode(Token token)
        {
            this.token = token;
        }
        public ReferenceAccessNode()
        {

        }
    }
}