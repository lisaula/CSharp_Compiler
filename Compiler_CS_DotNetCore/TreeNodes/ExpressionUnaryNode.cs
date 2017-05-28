using System;

namespace Compiler.Tree
{
    public class ExpressionUnaryNode : UnaryExpressionNode
    {
        public Token Operator;
        public UnaryExpressionNode expression;

        public ExpressionUnaryNode()
        {

        }
        public ExpressionUnaryNode(Token unaryOperator, UnaryExpressionNode unary_expression)
        {
            this.Operator = unaryOperator;
            this.expression = unary_expression;
        }
    }
}