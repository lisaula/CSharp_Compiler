using System;

namespace Compiler.Tree
{
    public class AccessMemory : PrimaryExpressionNode
    {
        public PrimaryExpressionNode expression;

        public AccessMemory()
        {
            
        }

        public AccessMemory(PrimaryExpressionNode id)
        {
            this.expression = id;
        }

        public override TypeDefinitionNode evaluateType()
        {
            return expression.evaluateType();
        }
    }
}