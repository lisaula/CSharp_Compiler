using System;

namespace Compiler.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditional_expression;
        public ExpressionNode true_expression;
        public ExpressionNode false_expression;

        public TernaryExpressionNode(ExpressionNode expr, ExpressionNode true_expression, ExpressionNode false_expression)
        {
            this.conditional_expression = expr;
            this.true_expression = true_expression;
            this.false_expression = false_expression;
        }
        public TernaryExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType()
        {
            throw new NotImplementedException();
        }
    }
}