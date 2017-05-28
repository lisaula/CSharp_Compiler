namespace Compiler.Tree
{
    public class AssignmentNode : ExpressionNode
    {
        public UnaryExpressionNode leftValue;
        public Token assigmentOperator;
        public ExpressionNode expr;

        public AssignmentNode(UnaryExpressionNode leftValue, Token assigmentOperator, ExpressionNode expr)
        {
            this.leftValue = leftValue;
            this.assigmentOperator = assigmentOperator;
            this.expr = expr;
        }
    }
}