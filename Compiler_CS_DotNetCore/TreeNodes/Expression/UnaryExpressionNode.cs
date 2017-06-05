using System;

namespace Compiler.Tree
{
    public class UnaryExpressionNode : ExpressionNode
    {
        public UnaryExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType()
        {
            throw new NotImplementedException();
        }
    }
}