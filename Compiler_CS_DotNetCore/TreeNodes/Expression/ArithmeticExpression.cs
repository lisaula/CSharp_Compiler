using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public class ArithmeticExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public Token @operator;
        public ExpressionNode rightExpression;

        public ArithmeticExpression(ExpressionNode leftExpression, Token @operator, ExpressionNode rightExpression)
        {
            this.leftExpression = leftExpression;
            this.@operator = @operator;
            this.rightExpression = rightExpression;
        }
        public ArithmeticExpression()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            throw new NotImplementedException();
        }
    }
}