using System;

namespace Compiler.Tree
{
    public class IsExpression : ExpressionNode
    {
        public ExpressionNode leftExpression;
        public TypeDefinitionNode type;

        public IsExpression(ExpressionNode leftExpression, TypeDefinitionNode type)
        {
            this.leftExpression = leftExpression;
            this.type = type;
        }

        public override TypeDefinitionNode evaluateType()
        {
            throw new NotImplementedException();
        }
        public IsExpression()
        {

        }
    }
}