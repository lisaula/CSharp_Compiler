using System;
using System.Collections.Generic;

namespace Compiler.Tree
{
    public class FunctionCallExpression : PrimaryExpressionNode
    {
        public PrimaryExpressionNode primary;
        public List<ExpressionNode> arguments;

        public FunctionCallExpression(PrimaryExpressionNode primary, List<ExpressionNode> arguments)
        {
            this.primary = primary;
            this.arguments = arguments;
        }
        public FunctionCallExpression()
        {

        }

        public FunctionCallExpression(List<ExpressionNode> arguments)
        {
            this.arguments = arguments;
        }

        public override TypeDefinitionNode evaluateType()
        {
            throw new NotImplementedException();
        }
    }
}