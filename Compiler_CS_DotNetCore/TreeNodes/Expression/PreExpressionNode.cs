using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class PreExpressionNode : UnaryExpressionNode
    {
        public Token Operator;
        public UnaryExpressionNode expression;

        public PreExpressionNode()
        {

        }
        public PreExpressionNode(Token unaryOperator, UnaryExpressionNode unary_expression)
        {
            this.Operator = unaryOperator;
            this.expression = unary_expression;
        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}