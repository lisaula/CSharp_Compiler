using Compiler_CS_DotNetCore.Semantic;
using System;

namespace Compiler.Tree
{
    public  class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expr;

        public ParenthesizedExpressionNode(ExpressionNode expr)
        {
            this.expr = expr;
        }
        public ParenthesizedExpressionNode()
        {

        }

        public override TypeDefinitionNode evaluateType(API api)
        {
            return expr.evaluateType(api);
        }
    }
}