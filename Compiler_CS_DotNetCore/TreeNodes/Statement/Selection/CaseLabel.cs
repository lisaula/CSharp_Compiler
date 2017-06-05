namespace Compiler.Tree
{
    public class CaseLabel
    {
        public Token token;
        public ExpressionNode expr;

        public CaseLabel(Token token, ExpressionNode expr)
        {
            this.token = token;
            this.expr = expr;
        }
        public CaseLabel()
        {

        }

        public CaseLabel(Token token)
        {
            this.token = token;
        }
    }
}